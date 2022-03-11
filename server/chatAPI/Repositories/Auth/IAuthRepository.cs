using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Repositories;

public interface IAuthRepository : IDisposable
{

    /// <summary>
    /// Get a query that when resolved returns all users
    /// </summary>
    /// <returns>A query to get all users</returns>
    IQueryable<User> GetAll();

    /// <summary>
    /// Get one using ID
    /// </summary>
    /// <returns>User</returns>
    User GetUserById(Guid id);

}