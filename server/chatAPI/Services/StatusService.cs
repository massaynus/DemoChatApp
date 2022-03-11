using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chatAPI.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace chatAPI.Services;

public class StatusService
{
    private readonly ConcurrentDictionary<Guid, string> _onlineUsers;
    private readonly ILogger<StatusService> _logger;

    public StatusService(ILogger<StatusService> logger)
    {
        _onlineUsers = new();
        _logger = logger;
    }

    public void AddOnlineUser(Guid id)
    {
        _onlineUsers.TryAdd(id, "online");
        _logger.LogInformation($"Added online user {id.ToString()}");
    }

    public void RemoveOnlineUser(Guid id)
    {
        _onlineUsers.TryUpdate(id, "offline", "online");
        _logger.LogInformation($"Removed online user {id.ToString()}");
    }

    public bool IsUserOnline(Guid id)
    {
        var exists = _onlineUsers.TryGetValue(id, out string status);
        return exists && status == "online";
    }

    public IEnumerable<Guid> GetOnlineUsersIDs()
    {
        _logger.LogInformation($"online users: {_onlineUsers.Count}");
        return _onlineUsers.Where(pair => pair.Value == "online").Select(pair => pair.Key);
    }
}