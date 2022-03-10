using AutoMapper;
using chatAPI.Data;
using chatAPI.Models;
using chatAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Services;

public class AuthService : IAuthService
{
    private bool disposedValue;
    private readonly ApplicationDbContext _appDb;

    private readonly IMapper _mapper;
    private readonly IUserRepository _usersRepository;
    private readonly IAuthRepository _authRepository;

    private readonly JwtService _jwt;
    private readonly CryptoService _crypto;

    public AuthService(
        ApplicationDbContext appDb,
        JwtService jwt,
        CryptoService crypto,
        IMapper mapper, IUserRepository usersRepository, IAuthRepository authRepository)
    {
        _appDb = appDb;
        _jwt = jwt;
        _crypto = crypto;
        _mapper = mapper;
        _usersRepository = usersRepository;
        _authRepository = authRepository;
    }

    public IEnumerable<DTOs.User> GetAll()
    {
        return _mapper.ProjectTo<DTOs.User>(
            _usersRepository.GetAll()
        );
    }

    public DTOs.User GetUserById(Guid id)
    {
        return _mapper.Map<DTOs.User>(
            _usersRepository.GetUserById(id)
        );
    }

    public DTOs.UserLoginResponse Authenticate(string username, string password)
    {
        return _authRepository.Authenticate(username, password);
    }

    public DTOs.UserLoginResponse Authenticate(DTOs.UserLoginRequest userLoginRequest)
    {
        return Authenticate(userLoginRequest.Username, userLoginRequest.Password);
    }

    public DTOs.User ChangePassword(Guid id, string oldPassword, string newPassword)
    {
        // I think this is beyond scope 😅
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