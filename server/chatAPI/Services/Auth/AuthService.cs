using AutoMapper;
using chatAPI.Data;
using chatAPI.DTOs;
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

    public IEnumerable<UserData> GetAll()
    {
        return _mapper.ProjectTo<UserData>(
            _usersRepository.GetAll()
        );
    }

    public UserData GetUserById(Guid id)
    {
        return _mapper.Map<UserData>(
            _usersRepository.GetUserById(id)
        );
    }

    public UserLoginResponse Authenticate(string username, string password)
    {
        var user = _appDb.Users
            .Include(u => u.Role)
            .AsSingleQuery()
            .FirstOrDefault(u => u.Username == username);

        if (user is null || !_crypto.Verify(user.Password, password))
            return new()
            {
                Username = username,
                JWTToken = null,
                OperationResult = UserLoginOperationResult.failure
            };

        var userData = _mapper.Map<UserData>(user);

        return new()
        {
            Username = username,
            JWTToken = _jwt.GenerateToken(userData),
            User = userData,
            OperationResult = UserLoginOperationResult.success
        };
    }

    public UserLoginResponse Authenticate(UserLoginRequest userLoginRequest)
    {
        return Authenticate(userLoginRequest.Username, userLoginRequest.Password);
    }

    public UserData ChangePassword(Guid id, string oldPassword, string newPassword)
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