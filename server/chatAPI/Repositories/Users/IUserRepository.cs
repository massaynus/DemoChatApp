using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Repositories;

public interface IUserRepository : IDisposable
{
    IQueryable<User> GetAll();

    IQueryable<User> GetUsersByStatus(string status);
    User GetUserById(Guid id);

    User CreateUser(User user);

    User UpdateUser(Guid id, User user);
    User UpdateUserStatus(User user, Status status);
    User UpdateUserStatus(Guid id, string status);

    User DeleteUser(Guid id);
}