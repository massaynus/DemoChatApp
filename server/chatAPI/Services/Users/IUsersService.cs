using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Services;

public interface IUserService : IDisposable
{
    IEnumerable<UserData> GetAll();
    IEnumerable<UserData> GetUsersByStatus(string status);
    UserData GetUserById(Guid id);

    UserData CreateUser(UserSignUpRequest user);

    UserData UpdateUser(Guid id, UserData user);
    UserData UpdateUserStatus(User user, Status status);
    UserData UpdateUserStatus(Guid id, string status);

    UserData DeleteUser(Guid id);
}