using chatAPI.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace chatAPI.Hubs;

public class StatusHub : Hub
{
    public async Task StatusChange(UserData data)
    {
        await Clients.All
            .SendAsync(nameof(StatusChange), data);
    }
}