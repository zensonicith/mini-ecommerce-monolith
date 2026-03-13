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

            var seeders = new List<ISeeder>
            {
                new CustomerSeeder(),
                new CustomerSeeder(),
                new ProductSeeder(),
                new OrderSeeder(),
                new OrderProductsSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(context);
            }
        }
    }
}