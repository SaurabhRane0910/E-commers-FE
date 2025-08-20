using blazornew.Components.Common;
using blazornew.Model.signup;
using blazornew.Service.IMP;
using System.Text.Json;
using System.Text;

namespace blazornew.Service
{
    public class SignupService : ISignupService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<SignupService> _logger;

        public SignupService(HttpClient httpClient, ILogger<SignupService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> RegisterUserAsync(SignupRequest request)
        {
            try
            {
                
                var payload = new
                {
                    name = request.Name,
                    email = request.Email,
                    password = request.Password,
                    phone_number = request.Phone_Number,
                    gender = request.Gender,
                    profile_image = request.Profile_Image,
                    address = new
                    {
                        country_id = request.Address.Country_Id,
                        state_id = request.Address.State_Id,
                        city_id = request.Address.City_Id,
                        postal_code = request.Address.Postal_Code,
                        label = request.Address.Label,
                        address_line1 = request.Address.Address_Line1,
                        address_line2 = request.Address.Address_Line2
                    }
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("user/registration", content);
                var rawContent = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Signup API raw response: {RawContent}", rawContent);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = $"Request failed: {response.StatusCode}",
                        Data = rawContent,
                        StatusCode = (int)response.StatusCode
                    };
                }

                
                using var doc = JsonDocument.Parse(rawContent);
                var root = doc.RootElement;

                var statusCode = root.GetProperty("statusCode").GetInt32();
                var message = root.GetProperty("message").GetString();
                var status = root.GetProperty("status").GetString();
                var success = string.Equals(status, "Success", StringComparison.OrdinalIgnoreCase);

                string? data = null;
                if (root.TryGetProperty("data", out var dataElement))
                {
                    data = dataElement.ToString(); 
                }

                return new ApiResponse<string>
                {
                    Success = success,
                    Message = message ?? string.Empty,
                    Data = data,
                    StatusCode = statusCode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during RegisterUserAsync");
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Exception: " + ex.Message,
                    Data = null,
                    StatusCode = 500
                };
            }
        }

        public async Task<List<LocationModel>> GetCountriesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<List<LocationModel>>>(
                "https://ecomm-intern-demo.onrender.com/api/listOfCountry");

            return result?.Data ?? new List<LocationModel>();
        }

        public async Task<List<StateModel>> GetStatesAsync(int countryId)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<List<StateModel>>>(
                $"https://ecomm-intern-demo.onrender.com/api/listOfState/{countryId}");

            return result?.Data ?? new List<StateModel>();
        }

        public async Task<List<CityModel>> GetCitiesAsync(int stateId)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<List<CityModel>>>(
                $"https://ecomm-intern-demo.onrender.com/api/listOfCity/{stateId}");

            return result?.Data ?? new List<CityModel>();
        }

        public async Task<HttpResponseMessage> UploadFileAsync(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();

           
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            content.Add(fileContent, "files", fileName);

            return await _httpClient.PostAsync("fileUpload", content);
        }



    }
}
