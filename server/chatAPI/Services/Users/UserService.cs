using AutoMapper;
using chatAPI.Repositories;
using chatAPI.DTOs;
using chatAPI.Models;

namespace chatAPI.Services;

public class UserService : IUserService
{
    private bool disposedValue;

    private readonly CryptoService _crypto;

    private readonly IMapper _mapper;
    private readonly IUserRepository _usersRepository;

    public UserService(IMapper mapper, CryptoService crypto, IUserRepository usersRepository)
    {
        _mapper = mapper;
        _crypto = crypto;
        _usersRepository = usersRepository;
    }

    public IEnumerable<UserData> GetUsersByStatus(string status)
    {
        return _mapper.ProjectTo<UserData>(
            _usersRepository.GetUsersByStatus(status)
        );
    }

    public IEnumerable<UserData> GetAll(int page)
    {
        return GetAll(page, 24);
    }

    public IEnumerable<UserData> GetAll(int page, int pageSize)
    {
        return _mapper.ProjectTo<UserData>(
            _usersRepository.GetAll()
                .OrderBy(u => u.Username)
                .Skip(page * pageSize)
                .Take(pageSize)
        );
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