using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Application.Interfaces.Repo;
using MyApp.Application.Services;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services;
using Stripe.BillingPortal;

namespace MyApp.Infrastructure
{
    public static class InfrasDi
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IFileStorageService, AzureBlobStorageService>();
            services.AddScoped<SessionService>();
            services.AddScoped<IPaymentService, StripePaymentService>();

            return services;
        }
    }
}