using chatAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace chatAPI.Hubs;

[Authorize]
public class StatusHub : Hub
{
    public async Task StatusChange(UserData data)
    {
        await Clients.All
            .SendAsync(nameof(StatusChange), data);
    }
}