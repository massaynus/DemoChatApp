using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IAuthRepository : IDisposable
{
    IEnumerable<DTOs.User> GetAll();
    DTOs.User GetUserById(Guid id);
    DTOs.User GetUserByAccountId(Guid id);

    Models.User CreateAccount(DTOs.User user);
    Models.User UpdateAccount(Guid id, DTOs.User user);
    Models.User DeleteAccount(Guid id);

    Models.User Authenticate(string username, string password);
    Models.User ChangePassword(Guid id, string oldPassword, string newPassword);

}