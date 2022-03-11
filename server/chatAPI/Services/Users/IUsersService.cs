using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Services;

public interface IUserService : IDisposable
{
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>IEnumerable<User></returns>
    UserDataList GetOnlineUsers();

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>IEnumerable<User></returns>
    UserDataList GetAll(int page);

    /// <summary>
    /// Get all users with the supplied status
    /// </summary>
    /// <param name="status">status filter</param>
    /// <returns>IEnumerable<User></returns>
    UserDataList GetUsersByStatus(string status, int page);

    /// <summary>
    /// Get one user using ID
    /// </summary>
    /// <param name="id">the user's ID</param>
    /// <returns>UserData</returns>
    UserData GetUserById(Guid id);

    /// <summary>
    /// Create one user in DB, if username is already used no harm is done
    /// </summary>
    /// <param name="user">user signup data</param>
    /// <returns>UserData</returns>
    UserData CreateUser(UserSignUpRequest user);

    /// <summary>
    /// Update a user in the database
    /// </summary>
    /// <param name="id">the target user's id</param>
    /// <param name="user">the new data</param>
    /// <returns>UserData</returns>
    UserData UpdateUser(Guid id, UserData user);

    /// <summary>
    /// Update a user's status
    /// </summary>
    /// <param name="id">user's id</param>
    /// <param name="status">the new status</param>
    /// <exception cref="InvalidStatusException">when the supplied status doens't exist in DB</exception>
    /// <returns>UserData</returns>
    UserData UpdateUserStatus(Guid id, string status);

    /// <summary>
    /// Delete a user from DB<br/>
    /// This is not a soft delete
    /// </summary>
    /// <param name="id">the user's id</param>
    /// <returns>UserData</returns>
    UserData DeleteUser(Guid id);
}