using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApp.API.Exception.Handler;
using MyApp.Infrastructure.Persistence;
using MyApp.Infrastructure.Persistence.Seed;
using MyApp.Infrastructure;
using MyApp.Application;
using MyApp.Infrastructure.Options;
using MyApp.Infrastructure.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// === Config Serilog ===
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

// === Config Exception Handler ===
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// === Config database connection ===
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// === Config CORS ===

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// === Register services ===
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

// === JWT Configuration ===
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));
var jwtSetting = builder.Configuration.GetSection("Jwt").Get<JwtSetting>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSetting.Issuer,
            ValidAudience = jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

// === Blob storage Configuration ===
builder.Services.Configure<BlobStorageOptions>(
    builder.Configuration.GetSection("AzureBlobStorage"));

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

app.UseExceptionHandler();

// === Logging each request + enrich description into log ===
app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("UserId",
            httpContext.User.FindFirst("id")?.Value);
        diagnosticContext.Set("ClientIP",
            httpContext.Connection.RemoteIpAddress?.ToString());
        diagnosticContext.Set("UserAgent",
            httpContext.Request.Headers.UserAgent.ToString());
        diagnosticContext.Set("RequestPath",
            httpContext.Request.Path);
    };
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();