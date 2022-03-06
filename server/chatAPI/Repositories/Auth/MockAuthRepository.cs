using chatAPI.Data;
using chatAPI.Models;

namespace chatAPI.Repositories;

public class MockAuthRepository : IAuthRepository
{
    private bool disposedValue;

    public MockAuthRepository()
    {
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

    public Account CreateAccount(DTOs.User user)
    {
        throw new NotImplementedException();
    }

    public Account UpdateAccount(Guid id, DTOs.User user)
    {
        throw new NotImplementedException();
    }

    public Account DeleteAccount(Guid id)
    {
        throw new NotImplementedException();
    }

    public Account Authenticate(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Account ChangePassword(Guid accountId, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        disposedValue = true;
        GC.SuppressFinalize(this);
    }
}