using DesignPatterns.Facade.TransparentFacadeSubSystem.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DesignPatterns.Facade.TransparentFacadeSubSystem;

public static class StartupExtensions
{
    public static IServiceCollection AddTransparentFacadeSubSystem(this IServiceCollection services)
    {
        services.TryAddSingleton<IInventoryService, InventoryService>();
        services.TryAddSingleton<IOrderProcessingService, OrderProcessingService>();
        services.TryAddSingleton<IShippingService, ShippingService>();
        services.TryAddSingleton<IECommerceTransparentFacade, ECommerceFacade>();
        return services;
    }
}
