using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Enum;

namespace MyApp.Infrastructure.Persistence.Seed;

public class CustomerSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        var adminRole = await context.Roles
            .FirstAsync(x => x.RoleType == ERole.ADMIN);

        var userRole = await context.Roles
            .FirstAsync(x => x.RoleType == ERole.USER);

        var customers = new List<Customer>
        {
            new Customer
            {
                Name = "John Doe", Address = "123 Street", City = "New York", UserName = "johndoe", Password = "123123",
                RoleId = adminRole.Id
            },
            new Customer
            {
                Name = "Jane Smith", Address = "456 Avenue", City = "Los Angeles", UserName = "janesmith",
                Password = "123123", RoleId = userRole.Id
            }
        };

        var existingUsers = await context.Customers
            .Select(x => x.UserName)
            .ToListAsync();

        var newCustomers = customers
            .Where(x => !existingUsers.Contains(x.UserName))
            .ToList();

        if (newCustomers.Count != 0)
        {
            await context.Customers.AddRangeAsync(newCustomers);
            await context.SaveChangesAsync();
        }
    }
}