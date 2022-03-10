using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IAuthRepository : IDisposable
{
    IQueryable<Models.User> GetAll();
    IQueryable<Models.User> GetUserById(Guid id);

    DTOs.UserLoginResponse Authenticate(DTOs.UserLoginRequest userLoginRequest);
    DTOs.UserLoginResponse Authenticate(string username, string password);

    IQueryable<Models.User> ChangePassword(Guid id, string oldPassword, string newPassword);

}