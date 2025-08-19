using blazornew.Model;


namespace blazornew.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
           
            try
            {
                Console.WriteLine("console from the login");
                var response = await _httpClient.PostAsJsonAsync("https://ecomm-intern-demo.onrender.com/api/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return data ?? new LoginResponse
                    {
                        StatusCode = 500,
                        Message = "No response from server"
                    };
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return error ?? new LoginResponse
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = "Login failed"
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
