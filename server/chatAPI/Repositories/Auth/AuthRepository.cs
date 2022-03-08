using chatAPI.Data;
using chatAPI.Models;

namespace chatAPI.Repositories;

public class AuthRepository : IAuthRepository
{
    private bool disposedValue;

    private readonly ApplicationDbContext _appDb;

    public AuthRepository(ApplicationDbContext appDb)
    {
        _appDb = appDb;
    }

    public IEnumerable<DTOs.User> GetAll()
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUserByAccountId(Guid id)
    {
        throw new NotImplementedException();
    }

    public User CreateAccount(DTOs.User user)
    {
        throw new NotImplementedException();
    }

    public User UpdateAccount(Guid id, DTOs.User user)
    {
        throw new NotImplementedException();
    }

    public User DeleteAccount(Guid id)
    {
        throw new NotImplementedException();
    }

    public User Authenticate(string username, string password)
    {
        throw new NotImplementedException();
    }

    public User ChangePassword(Guid id, string oldPassword, string newPassword)
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