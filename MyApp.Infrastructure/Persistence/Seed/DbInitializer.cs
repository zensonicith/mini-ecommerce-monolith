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