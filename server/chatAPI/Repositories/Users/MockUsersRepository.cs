using chatAPI.Data;

namespace chatAPI.Repositories;

public class MockUsersRepository : IUserRepository
{
    private bool disposedValue;

    public MockUsersRepository()
    {
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

    public void Dispose()
    {
        disposedValue = true;
        GC.SuppressFinalize(this);
    }
}