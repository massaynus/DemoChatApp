using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IUserRepository : IDisposable
{
    IEnumerable<DTOs.User> GetAll();
    IEnumerable<DTOs.User> GetUsersByStatus(string status);
    DTOs.User GetUserById(Guid id);

    DTOs.User CreateUser(DTOs.UserSignUpRequest user);

    DTOs.User UpdateUser(Guid id, DTOs.User user);
    DTOs.User UpdateUserStatus(Models.User user, Models.Status status);
    DTOs.User UpdateUserStatus(Guid id, string status);

    DTOs.User DeleteUser(Guid id);
}