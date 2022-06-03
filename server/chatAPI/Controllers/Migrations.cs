using chatAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace chatAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MigrationsController : ControllerBase
{
    private static bool HasRan = false;
    private readonly ILogger<AccountsController> _logger;
    private readonly ApplicationDbContext _db;

    public MigrationsController(
        ILogger<AccountsController> logger,
        ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpPost("/Run", Name = "Run")]
    public async Task<IActionResult> Run()
    {
        if (HasRan) return Forbid();
        HasRan = await _db.Database.EnsureCreatedAsync();

        return Ok($"Creation result: {HasRan}");
    }
}
