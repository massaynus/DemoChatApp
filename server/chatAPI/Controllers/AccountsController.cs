using chatAPI.Data;
using chatAPI.DTOs;
using chatAPI.Services;
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
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    private readonly CryptoService _cryptoService;
    private readonly JwtService _jwtService;

    public AccountsController(
        ILogger<AccountsController> logger,
        ApplicationDbContext db,
        CryptoService cryptoService,
        JwtService jwtService,
        IMapper mapper,
        IUserService userService,
        IAuthService authService)
    {
        _logger = logger;
        _db = db;
        _cryptoService = cryptoService;
        _jwtService = jwtService;
        _mapper = mapper;
        _userService = userService;
        _authService = authService;
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

        var result = _userService.CreateUser(userSignUpRequest);
        Response.StatusCode = StatusCodes.Status201Created;
        return _mapper.Map<UserSignUpResponse>(result);
    }

    [HttpPost("/SignIn", Name = "SignIn")]
    public UserLoginResponse SignIn(UserLoginRequest userLoginRequest)
    {
        return _authService.Authenticate(userLoginRequest);
    }
}
