using DesignPatterns.Facade.OpaqueFacadeSubSystem.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DesignPatterns.Facade.OpaqueFacadeSubSystem;

public static class StartupExtensions
{
    public static IServiceCollection AddOpaqueFacadeSubSystem(this IServiceCollection services)
    {
        services.AddSingleton<IECommerceOpaqueFacade>(_
            => new ECommerceFacade(new InventoryService(), new OrderProcessingService(), new ShippingService()));
        return services;
    }
}
