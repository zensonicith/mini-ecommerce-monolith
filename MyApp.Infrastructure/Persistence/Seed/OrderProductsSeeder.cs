using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed;

public class OrderProductsSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        var john = await context.Customers
            .FirstAsync(x => x.UserName == "johndoe");

        var order = await context.Orders
            .FirstAsync(x => x.CustomerId == john.Id);

        var laptop = await context.Products
            .FirstAsync(x => x.ProductName == "Laptop");

        var mouse = await context.Products
            .FirstAsync(x => x.ProductName == "Mouse");

        var items = new List<OrderProducts>
        {
            new OrderProducts
            {
                OrderId = order.Id,
                ProductId = laptop.Id,
                Quantity = 2
            },
            new OrderProducts
            {
                OrderId = order.Id,
                ProductId = mouse.Id,
                Quantity = 1
            }
        };

        foreach (var item in items)
        {
            bool exists = await context.OrderProducts.AnyAsync(x =>
                x.OrderId == item.OrderId &&
                x.ProductId == item.ProductId);

            if (!exists)
            {
                await context.OrderProducts.AddAsync(item);
            }
        }

        await context.SaveChangesAsync();
    }
}