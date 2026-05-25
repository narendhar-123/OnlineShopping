using Microsoft.Extensions.DependencyInjection;
using Shopping.Web.Service.Implementation;
using Shopping.Web.Service.Interfaces;
using Shopping.Web.Service.Payments.Factory;
using Shopping.Web.Service.Payments.Interfaces;

namespace Shopping.Web.Service.Extensions;

/// <summary>
/// Extension methods for registering service layer services.
/// </summary>
public static class ServiceLayerExtensions
{
    public static IServiceCollection AddServiceLayerServices(this IServiceCollection services)
    {
        // Register Payment Factory and Processors
        services.AddScoped<IPaymentFactory, PaymentFactory>();
        services.AddScoped<CreditCardPaymentProcessor>();
        services.AddScoped<UPIPaymentProcessor>();
        services.AddScoped<NetBankingPaymentProcessor>();

        // Register Item Service
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}
