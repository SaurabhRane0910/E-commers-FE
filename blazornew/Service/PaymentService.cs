using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace blazornew.Service
{
    public class PaymentService
    {
        private readonly HttpClient _http;

        public PaymentService(HttpClient http)
        {
            _http = http;
        }

        // ✅ Create PaymentIntent
        public async Task<CreatePaymentResponse?> CreatePaymentAsync(decimal amount)
        {
            var payload = new
            {
                amount = (int)(amount * 100), // Stripe requires cents
                currency = "usd",             // static
                paymentMethod = "pm_card_visa" // static test card
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var res = await _http.PostAsync(
                "https://ecomm-intern-demo.onrender.com/api/payment/create-payment",
                content
            );

            if (!res.IsSuccessStatusCode)
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ CreatePayment failed: {res.StatusCode} - {error}");
                return null;
            }

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CreatePaymentResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        // ✅ Verify PaymentIntent using payment_id
        public async Task<VerifyPaymentResponse?> VerifyPaymentAsync(string paymentId)
        {
            var payload = new { payment_id = paymentId };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var res = await _http.PostAsync(
                "https://ecomm-intern-demo.onrender.com/api/payment/verify-payment",
                content
            );

            if (!res.IsSuccessStatusCode)
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ VerifyPayment failed: {res.StatusCode} - {error}");
                return null;
            }

            var json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<VerifyPaymentResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }
    }

    // ✅ Models

    public class CreatePaymentResponse
    {
        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("payment_id")]
        public string PaymentId { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }
    }

    public class VerifyPaymentResponse
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public VerifyPaymentData Data { get; set; }
    }

    public class VerifyPaymentData
    {
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
    }
}
