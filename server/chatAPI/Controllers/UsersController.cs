using chatAPI.Data;
using chatAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ApplicationDbContext _db;

    public UsersController(ILogger<UsersController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> Get()
    {
        return _db.Users.ToList();
    }
}
