using chatAPI.Models;
using chatAPI.Data;

namespace chatAPI.Services;

public class UserAggregatorService : IUserAggregatorService
{
    private bool disposedValue;

    private readonly ApplicationDbContext _appDb;
    private readonly AuthDbContext _authDb;

    public UserAggregatorService(ApplicationDbContext appDb, AuthDbContext authDb)
    {
        _appDb = appDb;
        _authDb = authDb;
    }

    public DTOs.User BindUserAccount(Guid userId, Guid accountId)
    {
        throw new NotImplementedException();
    }

    public DTOs.User BindUserAccount(Models.User user, Account account)
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUser(Account account)
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUser(Models.User user)
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUserByAccountID(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUserByUserID(Guid userId)
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _appDb.Dispose();
                _authDb.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}