using chatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace chatAPI.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Status>().HasData(
            new Status {StatusName = "ON BREAK"},
            new Status {StatusName = "ON CALL"},
            new Status {StatusName = "IN MEETING"}
        );
    }
}