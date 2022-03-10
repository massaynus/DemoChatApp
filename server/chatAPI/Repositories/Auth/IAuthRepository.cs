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

    /// <summary>
    /// Authenticate a user
    /// </summary>
    /// <param name="userLoginRequest">the login informations</param>
    /// <returns>UserLoginResponse</returns>
    UserLoginResponse Authenticate(UserLoginRequest userLoginRequest);

    /// <summary>
    /// Authenticate a user
    /// </summary>
    /// <param name="username">the user's username</param>
    /// <param name="password">the user's passoword</param>
    /// <returns>UserLoginResponse</returns>
    UserLoginResponse Authenticate(string username, string password);

    /// <summary>
    /// Change a users passowrd. <br/>
    /// no change would be done if the oldPassword supplied is wrong
    /// </summary>
    /// <param name="id">the user ID</param>
    /// <param name="oldPassword">the old passowrd</param>
    /// <param name="newPassword">the new passowrd</param>
    /// <returns>User</returns>
    User ChangePassword(Guid id, string oldPassword, string newPassword);

}