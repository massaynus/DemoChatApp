using chatAPI.Data;
using chatAPI.DTOs;
using chatAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using chatAPI.Hubs;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IMapper _mapper;

    // Adding these in case i want to add user status features
    private readonly IHubContext<StatusHub> _statusHub;

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
        IAuthService authService,
        IHubContext<StatusHub> statusHub)
    {
        _logger = logger;
        _db = db;
        _cryptoService = cryptoService;
        _jwtService = jwtService;
        _mapper = mapper;
        _userService = userService;
        _authService = authService;
        _statusHub = statusHub;
    }

    [HttpPost("/SignUp", Name = "SignUp")]
    public async Task<UserSignUpResponse> SignUp(UserSignUpRequest userSignUpRequest)
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

        await _statusHub.Clients.All.SendAsync("UserCreated", result);
        return _mapper.Map<UserSignUpResponse>(result);
    }

    [HttpPost("/SignIn", Name = "SignIn")]
    public UserLoginResponse SignIn(UserLoginRequest userLoginRequest)
    {
        var result = _authService.Authenticate(userLoginRequest);
        return result;
    }

    [HttpPut("/ChangePassword", Name = "ChangePassword")]
    public UserData ChangePassword(ChangePasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OldPassword) ||
            string.IsNullOrWhiteSpace(request.NewPassword))
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return null;
        }

        return _authService.ChangePassword(request.ID, request.OldPassword, request.NewPassword);
    }
}
