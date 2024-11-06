using MinimalApiCrudWithDtos.Application.FakePersistance;

namespace MinimalApiCrudWithDtos.WebApi;

public static class StartupExtensions
{
    public static IServiceCollection AddOrderRepository(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, OrderRepository>();
        return services;
    }

    public static IApplicationBuilder InitializeSharedDataStore(this IApplicationBuilder app)
    {
        MemoryDataStore.Seed();
        return app;
    }
}
