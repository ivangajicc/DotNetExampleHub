using Baskets.Features;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Baskets;

public static class BasketModuleExtensions
{
    public static WebApplicationBuilder AddBasketModule(this WebApplicationBuilder builder)
    {
        builder.Services
            // Add features
            .AddAddItem()
            .AddFetchItems()
            .AddRemoveItem()
            .AddUpdateQuantity()

            // Add and configure db context
            .AddDbContext<BasketContext>(options => options
                .UseInMemoryDatabase("BasketContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning)))
        ;
        return builder;
    }

    public static IEndpointRouteBuilder MapBasketModule(this IEndpointRouteBuilder endpoints)
    {
        _ = endpoints
            .MapGroup(Constants.ModuleName.ToLower(System.Globalization.CultureInfo.InvariantCulture))
            .WithTags(Constants.ModuleName)
            .AddFluentValidationFilter()

            // Map endpoints
            .MapFetchItems()
            .MapAddItem()
            .MapUpdateQuantity()
            .MapRemoveItem()
        ;
        return endpoints;
    }

    public static void AddBasketModuleConsumers(this IRegistrationConfigurator configurator) => configurator.AddConsumers(typeof(ProductEventsConsumers));
}
