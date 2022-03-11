using AutoMapper;
using chatAPI.Data;
using chatAPI.Services;
using chatAPI.DTOs;
using chatAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using chatAPI.Hubs;

namespace chatAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMapper _mapper;
    private readonly IHubContext<StatusHub> _statusHub;

    private readonly ApplicationDbContext _db;
    private readonly IUserService _userService;

    private readonly JwtService _jwtService;

    public UsersController(
        ILogger<UsersController> logger,
        ApplicationDbContext db,
        IMapper mapper,
        JwtService jwtService,
        IUserService userService,
        IHubContext<StatusHub> statusHub)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
        _jwtService = jwtService;
        _userService = userService;
        _statusHub = statusHub;
    }

    [HttpGet("/api/[controller]/GetUsers/{page=0}", Name = "GetUsers")]
    public UserDataList GetAll([FromRoute] int page)
    {
        return _userService.GetAll(page);
    }

    [HttpGet("/api/[controller]/Statuses", Name = "Statuses")]
    public IEnumerable<Status> GetStatuses()
    {
        return _db.Statuses.AsEnumerable();
    }

    [HttpGet("/api/[controller]/GetUsersByStatus/{status}/{page=0}", Name = "GetUsersByStatus")]
    public UserDataList GetByStatus([FromRoute] string status, [FromRoute] int page)
    {
        return _userService.GetUsersByStatus(status, page);
    }

    [HttpPost("/api/[controller]/CreateStatus", Name = "CreateStatus")]
    public async Task<Status> CreateStatus([FromBody] Status status)
    {
        if (string.IsNullOrWhiteSpace(status.StatusName))
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return null;
        }

        if (_db.Statuses.Any(s => s.NormalizedStatusName == status.NormalizedStatusName))
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return null;
        }

        _db.Statuses.Add(status);
        _db.SaveChanges();

        await _statusHub.Clients.All.SendAsync("StatusAdded", status);

        return status;
    }

    [HttpPut("/api/[controller]/ChangeUserStatus", Name = "ChangeUserStatus")]
    public async Task<UserStatusChangeResponse> UpdateStatus(UserStatusChangeRequest request)
    {
        var userId = _jwtService.GetPPID(HttpContext.User.Claims);
        var result = _userService.UpdateUserStatus(userId, request.Status);

        await _statusHub.Clients.All.SendAsync("StatusChange", result);

        return _mapper.Map<UserStatusChangeResponse>(result);
    }

}
