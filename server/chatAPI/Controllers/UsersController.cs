using AutoMapper;
using chatAPI.Data;
using chatAPI.Repositories;
using chatAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _db;
    private readonly IUserRepository _userRepository;

    private readonly JwtService _jwtService;

    public UsersController(
        ILogger<UsersController> logger,
        ApplicationDbContext db,
        IUserRepository userRepository,
        IMapper mapper,
        JwtService jwtService)
    {
        _logger = logger;
        _db = db;
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<DTOs.User> GetAll()
    {
        return _userRepository.GetAll();
    }

    [HttpGet(Name = "GetUsersByStatus")]
    [Route("[controller]/[action]/{status}")]
    public IEnumerable<DTOs.User> GetByStatus([FromRoute] string status)
    {
        return _userRepository.GetUsersByStatus(status);
    }

    [HttpPost(Name = "ChangeUserStatus")]
    public DTOs.UserStatusChangeResponse UpdateStatus(DTOs.UserStatusChangeRequest request)
    {
        var userId = _jwtService.GetPPID(HttpContext.User.Claims);
        return (DTOs.UserStatusChangeResponse) _userRepository.UpdateUserStatus(userId, request.Status);
    }


}
