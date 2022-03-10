using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Services;

public interface IAuthService : IDisposable
{
    IEnumerable<DTOs.User> GetAll();
    DTOs.User GetUserById(Guid id);

    DTOs.UserLoginResponse Authenticate(DTOs.UserLoginRequest userLoginRequest);
    DTOs.UserLoginResponse Authenticate(string username, string password);

    DTOs.User ChangePassword(Guid id, string oldPassword, string newPassword);

}