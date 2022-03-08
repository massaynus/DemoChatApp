using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IAuthRepository : IDisposable
{
    IEnumerable<DTOs.User> GetAll();
    DTOs.User GetUserById(Guid id);

    DTOs.UserLoginResponse Authenticate(string username, string password);
    DTOs.User ChangePassword(Guid id, string oldPassword, string newPassword);

}