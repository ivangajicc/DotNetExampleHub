namespace Baskets.Features;

public static class UpdateQuantityExtensions
{

    public static IServiceCollection AddUpdateQuantity(this IServiceCollection services) => services
            .AddScoped<UpdateQuantityHandler>()
            .AddSingleton<UpdateQuantityMapper>()
        ;

    public static IEndpointRouteBuilder MapUpdateQuantity(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(
            "/",
            (UpdateQuantityCommand command, UpdateQuantityHandler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(command, cancellationToken));
        return endpoints;
    }
}
