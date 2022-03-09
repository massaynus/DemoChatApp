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

    [HttpPost(Name = "SignUp")]
    public UserSignUpResponse SignUp(UserSignUpRequest userSignUpRequest)
    {
        var result = _userRepository.CreateUser(userSignUpRequest);
        return _mapper.Map<UserSignUpResponse>(result);
    }

    [HttpPost(Name = "SignIn")]
    public UserLoginResponse SignIn(UserLoginRequest userLoginRequest)
    {
        return _authRepository.Authenticate(userLoginRequest);
    }
}
