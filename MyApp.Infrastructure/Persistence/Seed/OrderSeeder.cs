using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed;

public class OrderSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        var john = await context.Customers
            .FirstOrDefaultAsync(x => x.UserName == "johndoe");

        if (!context.Orders.Any())
        {
            var order = new Order
            {
                CustomerId = john.Id,
                OrderDate = DateTime.UtcNow
            };

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }
    }
}