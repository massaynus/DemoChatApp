using System.Runtime.Serialization;
using AutoMapper;
using chatAPI.Data;
using chatAPI.Services;
using chatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Repositories;

public class UserRepository : IUserRepository
{
    private bool disposedValue;

    private readonly CryptoService _crypto;
    private readonly ApplicationDbContext _appDb;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext appDb, IMapper mapper, CryptoService crypto)
    {
        _appDb = appDb;
        _mapper = mapper;
        _crypto = crypto;
    }

    public IQueryable<User> GetUsersByStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return null;

        string normalizedStatus = status.ToUpperInvariant();

        return _appDb.Users
            .Where(u => u.Status.NormalizedStatusName == normalizedStatus)
            .Include(u => u.Status)
            .AsSingleQuery();
    }

    public IQueryable<User> GetAll()
    {
        return _appDb.Users
                    .Include(u => u.Status)
                    .AsSingleQuery();
    }

    public User GetUserById(Guid id)
    {
        return _appDb.Users.FirstOrDefault(u => u.ID == id);
    }

    public User CreateUser(User user)
    {
        user.LastStatusChange = DateTime.UtcNow;

        user.Status = _appDb.Statuses.FirstOrDefault(s => s.NormalizedStatusName == Status.DEAFULT_STATUS);
        user.Role = _appDb.Roles.FirstOrDefault(r => r.RoleName == Role.DEFAULT_ROLE);

        _appDb.Users.Add(user);
        _appDb.SaveChanges();

        return user;
    }

    public User DeleteUser(Guid id)
    {
        // It's said that this is faster than using .Find()
        var user = _appDb.Users.FirstOrDefault(u => u.ID == id);

        if (user is not null)
        {
            _appDb.Users.Remove(user);
            _appDb.SaveChanges();
        }

        return user;
    }

    public User UpdateUser(Guid id, User user)
    {
        user.ID = id;

        _appDb.Users.Attach(user);
        _appDb.SaveChanges();

        return user;
    }

    public User UpdateUserStatus(Guid id, string statusName)
    {
        var user = _appDb.Users.FirstOrDefault(u => u.ID == id);
        if (user is null) throw new UnknownUserException(id);

        var status = _appDb.Statuses.FirstOrDefault(s => s.NormalizedStatusName == statusName.ToUpperInvariant());
        if (status is null) throw new InvalidStatusException(statusName);

        _appDb.Attach(user);

        user.Status = status;
        user.LastStatusChange = DateTime.UtcNow;

        _appDb.SaveChanges();

        return user;
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

[Serializable]
public class InvalidStatusException : Exception
{
    public InvalidStatusException(string status) : base($"Invalid status supplied: {status}")
    {
    }
}

[Serializable]
public class UnknownUserException : Exception
{
    public UnknownUserException()
    {
    }

    public UnknownUserException(Guid id) : this(id.ToString())
    {
    }

    public UnknownUserException(string usernameOrId) : base($"Unknown user queried: {usernameOrId}")
    {
    }
}