using blazornew.Components.Common;
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
    }
}
