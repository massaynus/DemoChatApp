using AutoMapper;
using chatAPI.Data;
using chatAPI.Services;
using chatAPI.DTOs;
using chatAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chatAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
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
        JwtService jwtService,
        IUserService userService)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpGet("/api/[controller]/GetUsers/{page=1}", Name = "GetUsers")]
    public IEnumerable<UserData> GetAll([FromRoute] int page)
    {
        return _userService.GetAll(page);
    }

    [HttpGet("/api/[controller]/Statuses", Name = "Statuses")]
    public IEnumerable<Status> GetStatuses()
    {
        return _db.Statuses.AsEnumerable();
    }

    [HttpGet("/api/[controller]/GetUsersByStatus/{status}", Name = "GetUsersByStatus")]
    public IEnumerable<UserData> GetByStatus([FromRoute] string status)
    {
        return _userService.GetUsersByStatus(status);
    }

    [HttpPost("/api/[controller]/ChangeUserStatus", Name = "ChangeUserStatus")]
    public UserStatusChangeResponse UpdateStatus(UserStatusChangeRequest request)
    {
        var userId = _jwtService.GetPPID(HttpContext.User.Claims);
        var result = _userService.UpdateUserStatus(userId, request.Status);
        return _mapper.Map<UserStatusChangeResponse>(result);
    }


}
