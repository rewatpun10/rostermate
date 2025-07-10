
using Microsoft.EntityFrameworkCore;
using RosterMate.Domain.Entities;
using RosterMate.Infrastructure.Seeders;

namespace RosterMate.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Staff> Staff { get; set; }
    public DbSet<EmploymentDetail> EmploymentDetails { get; set; }
    public DbSet<PayrollDetail> PayrollDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optionally configure entities with Fluent API here
        modelBuilder.Entity<Staff>()
        .HasOne(s => s.EmploymentDetail)
        .WithOne(ed => ed.Staff)
        .HasForeignKey<EmploymentDetail>(ed => ed.StaffId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Staff>()
        .HasOne(s => s.PayrollDetail)
        .WithOne(pd => pd.Staff)
        .HasForeignKey<PayrollDetail>(pd => pd.StaffId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
