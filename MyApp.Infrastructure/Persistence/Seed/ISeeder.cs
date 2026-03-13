namespace MyApp.Infrastructure.Persistence.Seed;

public interface ISeeder
{
    Task SeedAsync(AppDbContext context);
}