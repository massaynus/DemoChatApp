using DTOs = chatAPI.DTOs;
using Models = chatAPI.Models;

namespace chatAPI.Services;

public interface IUserAggregatorService : IDisposable
{
    DTOs.User GetUserByAccountID(Guid accountId);
    DTOs.User GetUser(Models.Account account);

    DTOs.User GetUserByUserID(Guid userId);
    DTOs.User GetUser(Models.User user);

    DTOs.User BindUserAccount(Guid userId, Guid accountId);
    DTOs.User BindUserAccount(Models.User user, Models.Account account);
}