using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Persistence;
using MyApp.Infrastructure.Persistence.Seed;
using MyApp.Infrastructure;
using MyApp.Application;

var builder = WebApplication.CreateBuilder(args);


// === Config database connection ===
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// === Register services ===
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// === Initialize data ===
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
