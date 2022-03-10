using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Repositories;

public interface IUserRepository : IDisposable
{
    /// <summary>
    /// Get a query that when resolved returns all users
    /// </summary>
    /// <returns>A query to get all users</returns>
    IQueryable<User> GetAll();

    /// <summary>
    /// Get a query that when resolved returns all users with the supplied status
    /// </summary>
    /// <param name="status">status filter</param>
    /// <returns>A query to get all users with the supplied status</returns>
    IQueryable<User> GetUsersByStatus(string status);

    /// <summary>
    /// Get one user using ID
    /// </summary>
    /// <param name="id">the user's ID</param>
    /// <returns>User</returns>
    User GetUserById(Guid id);

    /// <summary>
    /// Create one user in DB, if username is already used no harm is done
    /// </summary>
    /// <param name="user">user model</param>
    /// <returns>User</returns>
    User CreateUser(User user);

    /// <summary>
    /// Update a user in the database
    /// </summary>
    /// <param name="id">the target user's id</param>
    /// <param name="user">the new data</param>
    /// <returns>User</returns>
    User UpdateUser(Guid id, User user);

    /// <summary>
    /// Update a user's status
    /// </summary>
    /// <param name="id">user's id</param>
    /// <param name="status">the new status</param>
    /// <exception cref="InvalidStatusException">when the supplied status doens't exist in DB</exception>
    /// <returns>User</returns>
    User UpdateUserStatus(Guid id, string status);

    /// <summary>
    /// Delete a user from DB<br/>
    /// This is not a soft delete
    /// </summary>
    /// <param name="id">the user's id</param>
    /// <returns>User</returns>
    User DeleteUser(Guid id);
}