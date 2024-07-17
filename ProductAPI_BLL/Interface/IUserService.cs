using ProductAPI_Data.Dtos.Request;
using ProductAPI_Data.Entities;

namespace ProductAPI_BLL.Interface
{
    public interface IUserService
    {
        Task<User> RegisterUser(UserRegistrationRequest Request);
    }
}
