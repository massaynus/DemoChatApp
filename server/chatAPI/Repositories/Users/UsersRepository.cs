using System.Runtime.Serialization;
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

    public DTOs.User CreateUser(DTOs.User user)
    {
        Models.User newUser = _mapper.Map<Models.User>(user);
        newUser.LastStatusChange = DateTime.UtcNow;

        _appDb.Users.Add(newUser);
        _appDb.SaveChanges();

        return _mapper.Map<DTOs.User>(newUser);
    }

    public DTOs.User DeleteUser(Guid id)
    {
        // It's said that this is faster than using .Find()
        var user = _appDb.Users.FirstOrDefault(u => u.ID == id);

        if (user is not null)
        {
            _appDb.Users.Remove(user);
            _appDb.SaveChanges();
        }

        return _mapper.Map<DTOs.User>(user);
    }

    public DTOs.User UpdateUser(Guid id, DTOs.User user)
    {
        Models.User newUser = _mapper.Map<Models.User>(user);
        newUser.ID = id;

        _appDb.Users.Attach(newUser);
        _appDb.SaveChanges();

        return _mapper.Map<DTOs.User>(newUser);
    }

    public DTOs.User UpdateUserStatus(Guid id, Models.Status status)
    {
        var user = _appDb.Users.FirstOrDefault(u => u.ID == id);
        user.Status = status;

        _appDb.SaveChanges();

        return _mapper.Map<DTOs.User>(user);
    }

    public DTOs.User  UpdateUserStatus(Guid id, string statusName)
    {
        var status = _appDb.Statuses.FirstOrDefault(s => s.NormalizedStatusName == statusName.ToUpperInvariant());
        if (status is null) throw new InvalidStatusException(statusName);
        return UpdateUserStatus(id, status);
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
}