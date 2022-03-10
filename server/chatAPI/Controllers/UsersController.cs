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
    private readonly IUserService _userService;

    private readonly JwtService _jwtService;

    public UsersController(
        ILogger<UsersController> logger,
        ApplicationDbContext db,
        IMapper mapper,
        JwtService jwtService, IUserService userService)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpGet("/GetUsers", Name = "GetUsers")]
    public IEnumerable<DTOs.User> GetAll()
    {
        return _userService.GetAll();
    }

    [HttpGet("/Statuses", Name = "Statuses")]
    public IEnumerable<Models.Status> GetStatuses()
    {
        return _db.Statuses.AsEnumerable();
    }

    [HttpGet("/GetUsersByStatus/{status}", Name = "GetUsersByStatus")]
    public IEnumerable<DTOs.User> GetByStatus([FromRoute] string status)
    {
        return _userService.GetUsersByStatus(status);
    }

    [HttpPost("/ChangeUserStatus", Name = "ChangeUserStatus")]
    public DTOs.UserStatusChangeResponse UpdateStatus(DTOs.UserStatusChangeRequest request)
    {
        var userId = _jwtService.GetPPID(HttpContext.User.Claims);
        var result = _userService.UpdateUserStatus(userId, request.Status);
        return _mapper.Map<DTOs.UserStatusChangeResponse>(result);
    }


}
