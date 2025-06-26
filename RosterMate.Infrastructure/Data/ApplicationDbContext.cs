
using Microsoft.EntityFrameworkCore;
using RosterMate.Domain.Entities;

namespace RosterMate.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Optionally configure entities with Fluent API here
        modelBuilder.Entity<Staff>().ToTable("Staff");
    }
}
