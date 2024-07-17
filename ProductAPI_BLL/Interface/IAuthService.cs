using ProductAPI_Data.Dtos.Response;

namespace ProductAPI_BLL.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> ValidateUser(UserAuthenticationResponse response);

        Task<string> CreateToken();

    }
}
