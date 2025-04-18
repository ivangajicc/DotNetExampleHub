using System.Reflection;
using Aggregator.WebApi;
using API.HttpClient;
using Baskets;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Products;

var builder = WebApplication.CreateBuilder(args);

// Register fluent validation
builder.AddFluentValidationEndpointFilter();
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblies(
    [
        Assembly.GetExecutingAssembly(),
        Assembly.GetAssembly(typeof(BasketModuleExtensions)),
        Assembly.GetAssembly(typeof(ProductsModuleExtensions)),
    ])
;

builder.AddApiHttpClient();
builder.AddExceptionMapper();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.CustomSchemaIds(type => type.FullName?.Replace("+", ".")));

builder
    .AddBasketModule()
    .AddProductsModule()
;
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddBasketModuleConsumers();
});

var app = builder.Build();
app.UseExceptionMapper();
app
    .MapBasketModule()
    .MapProductsModule()
;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDarkSwaggerUi();
}

// Convenience endpoint, seeding the catalog
app.MapGet("/", async (IWebClient client, CancellationToken cancellationToken) =>
{
    await client.Catalog.CreateProductAsync(new("Banana", 0.30m), cancellationToken);
    await client.Catalog.CreateProductAsync(new("Apple", 0.79m), cancellationToken);
    await client.Catalog.CreateProductAsync(new("Habanero Pepper", 0.99m), cancellationToken);
    return new
    {
        Message = "Application started and catalog seeded. Do not refresh this page, or it will reseed the catalog.",
    };
});

app.Run();
