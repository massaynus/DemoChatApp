using AutoMapper;
using chatAPI.Repositories;

namespace chatAPI.Services;

public class UsersService : IUserService
{
    private bool disposedValue;

    private readonly CryptoService _crypto;

    private readonly IMapper _mapper;
    private readonly IUserRepository _usersRepository;

    public UsersService(IMapper mapper, CryptoService crypto, IUserRepository usersRepository)
    {
        _mapper = mapper;
        _crypto = crypto;
        _usersRepository = usersRepository;
    }

    public IEnumerable<DTOs.User> GetUsersByStatus(string status)
    {
        return _mapper.ProjectTo<DTOs.User>(
            _usersRepository.GetUsersByStatus(status)
        );
    }

    public IEnumerable<DTOs.User> GetAll()
    {
        return _mapper.ProjectTo<DTOs.User>(_usersRepository.GetAll());
    }

    public DTOs.User GetUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user is not null) return _mapper.Map<DTOs.User>(user);
        else return null;
    }

    public DTOs.User CreateUser(DTOs.UserSignUpRequest user)
    {
        Models.User newUser = _mapper.Map<Models.User>(user);
        newUser.Password = _crypto.Hash(user.Password);

        return _mapper.Map<DTOs.User>(
            _usersRepository.CreateUser(newUser)
        );
    }

    public DTOs.User DeleteUser(Guid id)
    {
        var user = _usersRepository.DeleteUser(id);
        return user is not null ? _mapper.Map<DTOs.User>(user) : null;
    }

    public DTOs.User UpdateUser(Guid id, DTOs.User user)
    {
        Models.User newUser = _mapper.Map<Models.User>(user);

        return _mapper.Map<DTOs.User>(
            _usersRepository.UpdateUser(id, newUser)
        );
    }

    public DTOs.User UpdateUserStatus(Models.User user, Models.Status status)
    {
        return _mapper.Map<DTOs.User>(
            _usersRepository.UpdateUserStatus(user, status)
        );
    }

    public DTOs.User UpdateUserStatus(Guid id, string statusName)
    {
        return _mapper.Map<DTOs.User>(
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