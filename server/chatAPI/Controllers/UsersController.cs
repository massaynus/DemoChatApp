using chatAPI.Data;
using chatAPI.Models;
using chatAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly CryptoService _cryptoService;

    public UsersController(ILogger<UsersController> logger, ApplicationDbContext db, CryptoService cryptoService)
    {
        _logger = logger;
        _db = db;
        _cryptoService = cryptoService;
    }

    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
        var p1 = _cryptoService.Hash("p1");
        var p2 = _cryptoService.Hash("p2");

        return Ok(new {
            p1, p2,
            varifP1vP1 = _cryptoService.Verify(p1, "p1"),
            varifP1vP2 = _cryptoService.Verify(p1, "p2"),
            // varifP1vP2 = _cryptoService.Verify(_cryptoService.Hash("p1"), "p2"),
        });
    }
}
