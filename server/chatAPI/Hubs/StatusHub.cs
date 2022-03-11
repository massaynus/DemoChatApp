using chatAPI.Data;
using chatAPI.DTOs;
using chatAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace chatAPI.Hubs;

[Authorize]
public class StatusHub : Hub
{
    private readonly ApplicationDbContext _db;
    private readonly StatusService _status;
    private readonly JwtService _jwt;
    private readonly UserService _userService;

    public StatusHub(ApplicationDbContext db, StatusService status, JwtService jwt, UserService userService)
    {
        _db = db;
        _status = status;
        _jwt = jwt;
        _userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        Guid ID = _jwt.GetPPID(Context.User.Claims);
        _status.AddOnlineUser(ID);

        var user = _userService.GetUserById(ID);

        await Clients.All.SendAsync("UserLoggedIn", user);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Guid ID = _jwt.GetPPID(Context.User.Claims);
        _status.RemoveOnlineUser(ID);

        await Clients.All
            .SendAsync("UserSignOff", ID);
    }

    public async Task StatusChange(UserData data)
    {
        await Clients.All
            .SendAsync(nameof(StatusChange), data);
    }
}