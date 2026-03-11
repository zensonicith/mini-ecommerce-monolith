using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Enum;

namespace MyApp.Infrastructure.Persistence.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            if (context.Customers.Any())
                return;
            // Role
            var roles = new List<Role>
            {
                new Role { RoleType = ERole.ADMIN, Description = "Role for admin" },
                new Role { RoleType = ERole.USER, Description = "Role for uesr" }
            };
            
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
            // Customers
            var customers = new List<Customer>
            {
                new Customer { Name = "John Doe", Address = "123 Street", City = "New York", Password = "123123", UserName = "johndoe", Role = roles[0]},
                new Customer { Name = "Jane Smith", Address = "456 Avenue", City = "Los Angeles", Password = "123123", UserName = "janesmith", Role = roles[1]}
            };

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();

            // Products
            var products = new List<Product>
            {
                new Product { ProductName = "Laptop", Description = "High performance laptop for work and gaming", Unit = 10, Price = 1200 },
                new Product { ProductName = "Mouse", Description = "Wireless optical mouse", Unit = 50, Price = 25 },
                new Product { ProductName = "Keyboard", Description = "Mechanical keyboard with RGB lighting", Unit = 40, Price = 45 },
                new Product { ProductName = "Monitor", Description = "24-inch Full HD monitor", Unit = 20, Price = 220 },
                new Product { ProductName = "USB Flash Drive", Description = "64GB USB 3.0 flash drive", Unit = 100, Price = 15 },
                new Product { ProductName = "External Hard Drive", Description = "1TB portable external hard drive", Unit = 30, Price = 80 },
                new Product { ProductName = "Headphones", Description = "Noise-cancelling over-ear headphones", Unit = 25, Price = 150 },
                new Product { ProductName = "Webcam", Description = "1080p HD webcam for video conferencing", Unit = 35, Price = 60 },
                new Product { ProductName = "Laptop Stand", Description = "Adjustable aluminum laptop stand", Unit = 45, Price = 35 },
                new Product { ProductName = "Docking Station", Description = "USB-C docking station with multiple ports", Unit = 15, Price = 120 }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            // Orders
            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = customers[0].Id,
                    OrderDate = new DateTime(2026,1,1)
                },
                new Order
                {
                    CustomerId = customers[1].Id,
                    OrderDate = new DateTime(2026,1,2)
                }
            };

            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();

            // OrderProducts (many-to-many)
            var orderProducts = new List<OrderProducts>
            {
                new OrderProducts { OrderId = orders[0].Id, ProductId = products[0].Id , Quantity = 2},
                new OrderProducts { OrderId = orders[0].Id, ProductId = products[1].Id , Quantity = 1},
                new OrderProducts { OrderId = orders[1].Id, ProductId = products[2].Id , Quantity = 3}
            };

            await context.OrderProducts.AddRangeAsync(orderProducts);
            await context.SaveChangesAsync();
        }
    }
}