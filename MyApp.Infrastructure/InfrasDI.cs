using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Application.Services;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Infrastructure
{
    public static class InfrasDI
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}
