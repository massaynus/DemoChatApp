using AutoMapper;
using chatAPI.Repositories;
using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Services;

public class UserService : IUserService
{
    private bool disposedValue;

    private const int PAGE_SIZE = 24;

    private readonly CryptoService _crypto;

    private readonly IMapper _mapper;
    private readonly IUserRepository _usersRepository;
    private readonly StatusService _statusService;

    public UserService(IMapper mapper, CryptoService crypto, IUserRepository usersRepository, StatusService statusService)
    {
        _mapper = mapper;
        _crypto = crypto;
        _usersRepository = usersRepository;
        _statusService = statusService;
    }

    public UserDataList GetOnlineUsers()
    {
        var ids = _statusService.GetOnlineUsersIDs();

        return new UserDataList()
        {
            page = 0,
            total = _usersRepository.GetAll().Where(u => ids.Any(id => u.ID == id)).Count(),
            Users = _mapper.ProjectTo<UserData>(
                _usersRepository.GetAll()
                .Where(u => ids.Any(id => u.ID == id)))
        };
    }

    public UserDataList GetUsersByStatus(string status, int page, int pageSize)
    {
        return new UserDataList()
        {
            page = 0,
            total = _usersRepository.GetUsersByStatus(status).Count(),
            Users = _mapper.ProjectTo<UserData>(
            _usersRepository.GetUsersByStatus(status)
                .OrderBy(u => u.Username)
                .Skip(page * pageSize)
                .Take(pageSize))
        };
    }

    public UserDataList GetUsersByStatus(string status, int page = 0)
    {
        return GetUsersByStatus(status, page, PAGE_SIZE);
    }

    public UserDataList GetAll(int page, int pageSize)
    {
        return new UserDataList()
        {
            page = 0,
            total = _usersRepository.GetAll().Count(),
            Users = _mapper.ProjectTo<UserData>(
            _usersRepository.GetAll()
                .OrderBy(u => u.Username)
                .Skip(page * pageSize)
                .Take(pageSize))
        };
    }

    public UserDataList GetAll(int page = 0)
    {
        return GetAll(page, PAGE_SIZE);
    }

    public UserData GetUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user is not null) return _mapper.Map<UserData>(user);
        else return null;
    }

    public UserData CreateUser(UserSignUpRequest user)
    {
        User newUser = _mapper.Map<User>(user);
        newUser.Password = _crypto.Hash(user.Password);

        return _mapper.Map<UserData>(
            _usersRepository.CreateUser(newUser)
        );
    }

    public UserData DeleteUser(Guid id)
    {
        var user = _usersRepository.DeleteUser(id);
        return user is not null ? _mapper.Map<UserData>(user) : null;
    }

    public UserData UpdateUser(Guid id, UserData user)
    {
        User newUser = _mapper.Map<User>(user);

        return _mapper.Map<UserData>(
            _usersRepository.UpdateUser(id, newUser)
        );
    }

    public UserData UpdateUserStatus(Guid id, string statusName)
    {
        return _mapper.Map<UserData>(
            _usersRepository.UpdateUserStatus(id, statusName)
        );
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
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