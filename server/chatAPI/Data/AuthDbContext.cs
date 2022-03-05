using chatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Data;

public class AuthDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().HasData(
            new Role {RoleName = "User"}
        );
    }

}