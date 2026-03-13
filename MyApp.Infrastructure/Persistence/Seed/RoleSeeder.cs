using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Enum;

namespace MyApp.Infrastructure.Persistence.Seed;

public class RoleSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        var roles = new List<Role>
        {
            new Role { RoleType = ERole.ADMIN, Description = "Role for admin" },
            new Role { RoleType = ERole.USER, Description = "Role for user" }
        };

        var existing = await context.Roles
            .Select(x => x.RoleType)
            .ToListAsync();

        var newRoles = roles
            .Where(x => !existing.Contains(x.RoleType))
            .ToList();

        if (newRoles.Count != 0)
        {
            await context.Roles.AddRangeAsync(newRoles);
            await context.SaveChangesAsync();
        }
    }
}