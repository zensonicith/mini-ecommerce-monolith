using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed;

public class ProductSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        var products = new List<Product>
        {
            new Product
            {
                ProductName = "Laptop", Description = "High performance laptop for work and gaming", Unit = 10,
                Price = 1200
            },
            new Product { ProductName = "Mouse", Description = "Wireless optical mouse", Unit = 50, Price = 25 },
            new Product
            {
                ProductName = "Keyboard", Description = "Mechanical keyboard with RGB lighting", Unit = 40, Price = 45
            },
            new Product { ProductName = "Monitor", Description = "24-inch Full HD monitor", Unit = 20, Price = 220 },
            new Product
            {
                ProductName = "USB Flash Drive", Description = "64GB USB 3.0 flash drive", Unit = 100, Price = 15
            },
            new Product
            {
                ProductName = "External Hard Drive", Description = "1TB portable external hard drive", Unit = 30,
                Price = 80
            },
            new Product
            {
                ProductName = "Headphones", Description = "Noise-cancelling over-ear headphones", Unit = 25, Price = 150
            },
            new Product
            {
                ProductName = "Webcam", Description = "1080p HD webcam for video conferencing", Unit = 35, Price = 60
            },
            new Product
            {
                ProductName = "Laptop Stand", Description = "Adjustable aluminum laptop stand", Unit = 45, Price = 35
            },
            new Product
            {
                ProductName = "Docking Station", Description = "USB-C docking station with multiple ports", Unit = 15,
                Price = 120
            }
        };

        var existingProducts = await context.Products
            .Select(p => p.ProductName)
            .ToListAsync();

        var newProducts = products
            .Where(p => !existingProducts.Contains(p.ProductName))
            .ToList();

        if (newProducts.Count != 0)
        {
            await context.Products.AddRangeAsync(newProducts);
            await context.SaveChangesAsync();
        }
    }
}