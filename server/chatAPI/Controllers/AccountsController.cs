using chatAPI.Data;
using chatAPI.DTOs;
using chatAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly CryptoService _cryptoService;
    private readonly JwtService _jwtService;

    public AccountsController(
        ILogger<AccountsController> logger,
        ApplicationDbContext db,
        CryptoService cryptoService, JwtService jwtService)
    {
        _logger = logger;
        _db = db;
        _cryptoService = cryptoService;
        _jwtService = jwtService;
    }

    [HttpPost(Name = "SignUp")]
    public UserSignUpResponse SignUp(UserSignUpRequest userSignUpRequest)
    {
        throw new NotImplementedException();
    }

    [HttpPost(Name = "SignIn")]
    public UserLoginResponse SignIn(UserLoginRequest userLoginRequest)
    {
        throw new NotImplementedException();
    }
}
