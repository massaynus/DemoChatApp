using System.Runtime.Serialization;
using AutoMapper;
using chatAPI.Data;
using chatAPI.Services;
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

    public IQueryable<Models.User> GetUsersByStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return null;

        string normalizedStatus = status.ToUpperInvariant();

        return _appDb.Users
            .Where(u => u.Status.NormalizedStatusName == normalizedStatus)
            .Include(u => u.Status)
            .AsSingleQuery();
    }

    public IQueryable<Models.User> GetAll()
    {
        return _appDb.Users
                    .Include(u => u.Status)
                    .AsSingleQuery();
    }

    public Models.User GetUserById(Guid id)
    {
        return _appDb.Users.FirstOrDefault(u => u.ID == id);
    }

    public Models.User CreateUser(Models.User user)
    {
        user.LastStatusChange = DateTime.UtcNow;

        user.Status = _appDb.Statuses.FirstOrDefault(s => s.NormalizedStatusName == Models.Status.DEAFULT_STATUS);
        user.Role = _appDb.Roles.FirstOrDefault(r => r.RoleName == Models.Role.DEFAULT_ROLE);

        _appDb.Users.Add(user);
        _appDb.SaveChanges();

        return user;
    }

    public Models.User DeleteUser(Guid id)
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

    public Models.User UpdateUser(Guid id, Models.User user)
    {
        user.ID = id;

        _appDb.Users.Attach(user);
        _appDb.SaveChanges();

        return user;
    }

    public Models.User UpdateUserStatus(Models.User user, Models.Status status)
    {
        _appDb.Attach(user);

        user.Status = status;
        user.LastStatusChange = DateTime.UtcNow;

        _appDb.SaveChanges();

        return user;
    }

    public Models.User  UpdateUserStatus(Guid id, string statusName)
    {
        var user = _appDb.Users.FirstOrDefault(u => u.ID == id);
        if (user is null) throw new UnknownUserException(id);

        var status = _appDb.Statuses.FirstOrDefault(s => s.NormalizedStatusName == statusName.ToUpperInvariant());
        if (status is null) throw new InvalidStatusException(statusName);

        return UpdateUserStatus(user, status);
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

    [Serializable]
    private class InvalidStatusException : Exception
    {
        public InvalidStatusException(string status) : base($"Invalid status supplied: {status}")
        {
        }
    }

    [Serializable]
    private class UnknownUserException : Exception
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
}