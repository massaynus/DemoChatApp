using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chatAPI.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace chatAPI.Services;

public class StatusService
{
    private readonly SortedSet<Guid> _onlineUsers;

    public StatusService()
    {
        _onlineUsers = new();
    }

    public void AddOnlineUser(Guid id)
    {
        _onlineUsers.Add(id);
    }

    public void RemoveOnlineUser(Guid id)
    {
        _onlineUsers.Remove(id);
    }

    public bool IsUserOnline(Guid id)
    {
        return _onlineUsers.Contains(id);
    }

    public IEnumerable<Guid> GetOnlineUsersIDs()
    {
        return _onlineUsers.AsEnumerable();
    }
}