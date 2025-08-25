using blazornew.Components.Common;
using blazornew.Model;
using blazornew.Model.EditProfile;
using blazornew.Model.signup;
using System.Threading.Tasks;

namespace blazornew.Service.IMP
{
    public interface ISignupService
    {
        Task<ApiResponse<string>> RegisterUserAsync(SignupRequest request);
        Task<List<LocationModel>> GetCountriesAsync();
        Task<List<StateModel>> GetStatesAsync(int countryId);
        Task<List<CityModel>> GetCitiesAsync(int stateId);

        Task<HttpResponseMessage> UploadFileAsync(Stream fileStream, string fileName);

        Task<ApiResponse<object>> ResetPasswordAsync(ResetPasswordRequest request);


        Task<ApiResponse<object>> UpdateProfileAsync(UpdateProfileRequest request);
    }
}
