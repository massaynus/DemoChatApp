using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IUserRepository : IDisposable
{
    IEnumerable<DTOs.User> GetAll();
    DTOs.User GetUserById(Guid id);
    DTOs.User GetUserByAccountId(Guid id);

    Models.User CreateUser(DTOs.User user);
    Models.User UpdateUser(Guid id, DTOs.User user);
    Models.User DeleteUser(Guid id);
}