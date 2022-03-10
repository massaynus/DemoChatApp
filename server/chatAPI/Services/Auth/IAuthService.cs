using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Services;

public interface IAuthService : IDisposable
{
    IEnumerable<UserData> GetAll();
    UserData GetUserById(Guid id);

    UserLoginResponse Authenticate(UserLoginRequest userLoginRequest);
    UserLoginResponse Authenticate(string username, string password);

    UserData ChangePassword(Guid id, string oldPassword, string newPassword);

}