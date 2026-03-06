using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Persistence.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            if (context.Customers.Any())
                return;

            // Customers
            var customers = new List<Customer>
            {
                new Customer { Name = "John Doe", Address = "123 Street", City = "New York" },
                new Customer { Name = "Jane Smith", Address = "456 Avenue", City = "Los Angeles" }
            };

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();


            // Products
            var products = new List<Product>
            {
                new Product { ProductName = "Laptop", Unit = 10, Price = 1200 },
                new Product { ProductName = "Mouse", Unit = 50, Price = 25 },
                new Product { ProductName = "Keyboard", Unit = 40, Price = 45 }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();


            // Orders
            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = customers[0].Id,
                    Quantity = 2,
                    OrderDate = new DateTime(2026,1,1)
                },
                new Order
                {
                    CustomerId = customers[1].Id,
                    Quantity = 1,
                    OrderDate = new DateTime(2026,1,2)
                }
            };

            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();


            // OrderProducts (many-to-many)
            var orderProducts = new List<OrderProducts>
            {
                new OrderProducts { OrderId = orders[0].Id, ProductId = products[0].Id },
                new OrderProducts { OrderId = orders[0].Id, ProductId = products[1].Id },
                new OrderProducts { OrderId = orders[1].Id, ProductId = products[2].Id }
            };

            await context.OrderProducts.AddRangeAsync(orderProducts);
            await context.SaveChangesAsync();
        }
    }
}