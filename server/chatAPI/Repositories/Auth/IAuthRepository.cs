using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Repositories;

public interface IAuthRepository : IDisposable
{
    IQueryable<User> GetAll();
    User GetUserById(Guid id);

    UserLoginResponse Authenticate(UserLoginRequest userLoginRequest);
    UserLoginResponse Authenticate(string username, string password);

    User ChangePassword(Guid id, string oldPassword, string newPassword);

}