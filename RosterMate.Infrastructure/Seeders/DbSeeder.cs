
using Microsoft.EntityFrameworkCore;
using RosterMate.Domain.Entities;
using RosterMate.Domain.Enums;
using RosterMate.Infrastructure.Data;
using RosterMate.Domain.Helpers;

namespace RosterMate.Infrastructure.Seeders;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        if (!await dbContext.Staff.AnyAsync(s => s.Email == "superadmin@rostermate.com"))
        {
            var (passwordHash, passwordSalt) = PasswordHelper.HashPassword("Admin@123");

            var superAdmin = new Staff
            {
                FirstName = "Super",
                MiddleName = "Admin",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "superadmin@rostermate.com",
                MobileNumber = "1234567890",
                Role = Role.SuperAdmin,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

            };

            dbContext.Staff.Add(superAdmin);
            await dbContext.SaveChangesAsync();
        }
    }
}