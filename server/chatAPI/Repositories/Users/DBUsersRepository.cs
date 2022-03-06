using chatAPI.Data;

namespace chatAPI.Repositories;

public class DBUsersRepository : IUserRepository
{
    private bool disposedValue;

    private readonly ApplicationDbContext _appDb;
    private readonly AuthDbContext _authDb;

    public DBUsersRepository(ApplicationDbContext appDb, AuthDbContext authDb)
    {
        _appDb = appDb;
        _authDb = authDb;
    }

    public IEnumerable<DTOs.User> GetUsersByStatus(string status)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DTOs.User> GetAll()
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUserByAccountId(Guid id)
    {
        throw new NotImplementedException();
    }

    public DTOs.User GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.User CreateUser(DTOs.User user)
    {
        throw new NotImplementedException();
    }

    public Models.User DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public Models.User UpdateUser(Guid id, DTOs.User user)
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