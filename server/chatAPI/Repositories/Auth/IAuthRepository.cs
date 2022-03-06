using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IAuthRepository : IDisposable
{
    IEnumerable<DTOs.User> GetAll();
    DTOs.User GetUserById(Guid id);
    DTOs.User GetUserByAccountId(Guid id);

    Models.Account CreateAccount(DTOs.User user);
    Models.Account UpdateAccount(Guid id, DTOs.User user);
    Models.Account DeleteAccount(Guid id);

    Models.Account Authenticate(string username, string password);
    Models.Account ChangePassword(Guid accountId, string oldPassword, string newPassword);

}