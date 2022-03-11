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

    public StatusHub(ApplicationDbContext db, StatusService status, JwtService jwt)
    {
        _db = db;
        _status = status;
        _jwt = jwt;
    }

    public override Task OnConnectedAsync()
    {
        Guid ID = _jwt.GetPPID(Context.User.Claims);
        _status.AddOnlineUser(ID);

        return Task.CompletedTask;
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