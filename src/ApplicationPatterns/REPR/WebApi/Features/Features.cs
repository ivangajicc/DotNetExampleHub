using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using REPR.WebApi.Features.Baskets;
using REPR.WebApi.Features.Products;

namespace REPR.WebApi.Features;

public static class Features
{
    public static IServiceCollection AddFeatures(this WebApplicationBuilder builder)
    {
        // Register fluent validation
        builder.AddFluentValidationEndpointFilter();
        return builder.Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())

            // Add features
            .AddBasketsFeature()
            .AddProductsFeature()
        ;
    }

    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/")
            .AddFluentValidationFilter();
        ;
        group
            .MapBasketsFeature()
        ;
        return endpoints;
    }

    public static async Task SeedFeaturesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        await scope.SeedBasketsAsync();
        await scope.SeedProductsAsync();
    }
}
