using Microsoft.EntityFrameworkCore.Diagnostics;
using Products.Features;

namespace Products;

public static class ProductsModuleExtensions
{
    public static WebApplicationBuilder AddProductsModule(this WebApplicationBuilder builder)
    {
        builder.Services
            // Add features
            .AddFetchAll()
            .AddFetchOneProduct()
            .AddCreateProduct()
            .AddDeleteProduct()

            // Add and configure db context
            .AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("ProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning)))
        ;
        return builder;
    }

    public static IEndpointRouteBuilder MapProductsModule(this IEndpointRouteBuilder endpoints)
    {
        _ = endpoints
            .MapGroup(Constants.ModuleName.ToLower(System.Globalization.CultureInfo.InvariantCulture))
            .WithTags(Constants.ModuleName)
            .AddFluentValidationFilter()

            // Map endpoints
            .MapFetchAll()
            .MapFetchOneProduct()
            .MapCreateProduct()
            .MapDeleteProduct()
        ;
        return endpoints;
    }
}
