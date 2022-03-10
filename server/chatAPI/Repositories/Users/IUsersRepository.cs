using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Repositories;

public interface IUserRepository : IDisposable
{
    IQueryable<Models.User> GetAll();
    IQueryable<Models.User> GetUsersByStatus(string status);

    Models.User GetUserById(Guid id);

    Models.User CreateUser(Models.User user);

    Models.User UpdateUser(Guid id, Models.User user);
    Models.User UpdateUserStatus(Models.User user, Models.Status status);
    Models.User UpdateUserStatus(Guid id, string status);

    Models.User DeleteUser(Guid id);
}