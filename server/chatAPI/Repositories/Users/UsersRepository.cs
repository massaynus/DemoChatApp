using AutoMapper;
using chatAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Repositories;

public class UsersRepository : IUserRepository
{
    private bool disposedValue;

    private readonly ApplicationDbContext _appDb;
    private readonly IMapper _mapper;

    public UsersRepository(ApplicationDbContext appDb,  IMapper mapper)
    {
        _appDb = appDb;
        _mapper = mapper;
    }

    public IEnumerable<DTOs.User> GetUsersByStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return null;

        string normalizedStatus = status.ToUpperInvariant();

        return _appDb.Users
            .Where(u => u.Status.NormalizedStatusName == normalizedStatus)
            .Include(u => u.Status)
            .AsSingleQuery()
            .Select(u => _mapper.Map<DTOs.User>(u))
            .AsEnumerable();
    }

    public IEnumerable<DTOs.User> GetAll()
    {
        return _appDb.Users
                    .Include(u => u.Status)
                    .AsSingleQuery()
                    .Select(u => _mapper.Map<DTOs.User>(u))
                    .AsEnumerable();
    }

    public DTOs.User GetUserById(Guid id)
    {
        var user = _appDb.Users.FirstOrDefault(u => u.ID == id);
        if (user is not null) return _mapper.Map<DTOs.User>(user);
        else return null;
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