using chatAPI.Data;
using chatAPI.DTOs;
using chatAPI.Services;
using chatAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _db;
    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;

    private readonly CryptoService _cryptoService;
    private readonly JwtService _jwtService;

    public AccountsController(
        ILogger<AccountsController> logger,
        ApplicationDbContext db,
        CryptoService cryptoService,
        JwtService jwtService,
        IUserRepository userRepository,
        IAuthRepository authRepository,
        IMapper mapper)
    {
        _logger = logger;
        _db = db;
        _cryptoService = cryptoService;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _authRepository = authRepository;
        _mapper = mapper;
    }

    [HttpPost("/SignUp", Name = "SignUp")]
    public UserSignUpResponse SignUp(UserSignUpRequest userSignUpRequest)
    {
        if (_db.Users.Any(u => u.Username == userSignUpRequest.Username))
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new()
            {
                Message = "Username is taken"
            };
        }

        var result = _userRepository.CreateUser(userSignUpRequest);
        Response.StatusCode = StatusCodes.Status201Created;
        return _mapper.Map<UserSignUpResponse>(result);
    }

    [HttpPost("/SignIn", Name = "SignIn")]
    public UserLoginResponse SignIn(UserLoginRequest userLoginRequest)
    {
        return _authRepository.Authenticate(userLoginRequest);
    }
}
