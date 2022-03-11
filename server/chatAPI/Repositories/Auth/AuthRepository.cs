using AutoMapper;
using chatAPI.Data;
using chatAPI.Models;
using chatAPI.Services;

namespace chatAPI.Repositories;

public class AuthRepository : IAuthRepository
{
    private bool disposedValue;
    private readonly ApplicationDbContext _appDb;

    private readonly IUserRepository _usersRepository;

    private readonly JwtService _jwt;
    private readonly CryptoService _crypto;

    public AuthRepository(
        ApplicationDbContext appDb,
        JwtService jwt,
        CryptoService crypto,
        IUserRepository usersRepository,
        IMapper mapper)
    {
        _appDb = appDb;
        _jwt = jwt;
        _crypto = crypto;
        _usersRepository = usersRepository;
    }

    public IQueryable<User> GetAll()
    {
        return _usersRepository.GetAll();
    }

    public User GetUserById(Guid id)
    {
        return _usersRepository.GetUserById(id);
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