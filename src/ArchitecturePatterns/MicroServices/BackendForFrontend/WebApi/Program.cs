using System.Collections.Concurrent;
using System.Net;
using BackendForFrontend.WebApi;
using Baskets.WebApi;
using Refit;
using BasketsImport = Baskets.WebApi.Features.Baskets.Baskets;

var builder = WebApplication.CreateBuilder(args);
var basketsBaseAddress = builder.Configuration
    .GetValue<string>("Downstream:Baskets:BaseAddress") ?? throw new NotSupportedException("Cannot start the program without a Baskets base address.");
var productsBaseAddress = builder.Configuration
    .GetValue<string>("Downstream:Products:BaseAddress") ?? throw new NotSupportedException("Cannot start the program without a Products base address.");

builder.Services
    .AddRefitClient<IBasketsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(basketsBaseAddress))
;
builder.Services
    .AddRefitClient<IProductsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(productsBaseAddress))
;
builder.Services.AddTransient<IWebClient, DefaultWebClient>();
builder.Services.AddScoped<ICurrentCustomerService, FakeCurrentCustomerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.CustomSchemaIds(type => type.FullName?.Replace("+", ".")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDarkSwaggerUi();
}

app.MapGet(
    "api/catalog",
    (IWebClient client, CancellationToken cancellationToken)
        => client.Catalog.FetchProductsAsync(cancellationToken));
app.MapGet(
    "api/catalog/{productId}",
    (int productId, IWebClient client, CancellationToken cancellationToken)
        => client.Catalog.FetchProductAsync(new(productId), cancellationToken));

app.MapGet(
    "api/cart",
    async (IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken) =>
    {
        var logger = app.Services
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("GetCart");

        var basketItems = await client.Baskets.FetchCustomerBasketAsync(
            new(currentCustomer.Id),
            cancellationToken);

        var result = new ConcurrentBag<BasketProduct>();
        await Parallel.ForEachAsync(basketItems, cancellationToken, async (item, cancellationToken) =>
        {
            logger.LogTrace("Fetching product '{ProductId}'.", item.ProductId);
            var product = await client.Catalog.FetchProductAsync(
                new(item.ProductId),
                cancellationToken);
            logger.LogTrace("Found product '{ProductId}' ({ProductName}).", item.ProductId, product.Name);
            result.Add(new BasketProduct(
                product.Id,
                product.Name,
                product.UnitPrice,
                item.Quantity));
        });
        return result;
    });
app.MapPost(
    "api/cart",
    async (UpdateCartItem item, IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken) =>
    {
        if (item.Quantity <= 0)
        {
            await RemoveItemFromCart(
                item,
                client,
                currentCustomer,
                cancellationToken);
        }
        else
        {
            await AddOrUpdateItem(
                item,
                client,
                currentCustomer,
                cancellationToken);
        }

        return Results.Ok();
    });

app.Run();

static async Task RemoveItemFromCart(UpdateCartItem item, IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken)
{
    try
    {
        var result = await client.Baskets.RemoveProductFromCartAsync(
            new BasketsImport.RemoveItem.Command(
                currentCustomer.Id,
                item.ProductId),
            cancellationToken);
    }
    catch (ValidationApiException ex)
    {
        // If the product is not in the cart, it does not matter. In this case
        // we don't want to display any error in the UI. If its another exception,
        // we let it propagate up the stack.
        if (ex.StatusCode != HttpStatusCode.NotFound)
        {
            throw;
        }
    }
}

static async Task AddOrUpdateItem(UpdateCartItem item, IWebClient client, ICurrentCustomerService currentCustomer, CancellationToken cancellationToken)
{
    try
    {
        // Add the product to the cart
        await client.Baskets.AddProductToCartAsync(
            new BasketsImport.AddItem.Command(
                currentCustomer.Id,
                item.ProductId,
                item.Quantity),
            cancellationToken);
    }
    catch (ValidationApiException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
    {
        // Update the cart
        await client.Baskets.UpdateProductQuantityAsync(
            new BasketsImport.UpdateQuantity.Command(
                currentCustomer.Id,
                item.ProductId,
                item.Quantity),
            cancellationToken);
    }
}

public record class UpdateCartItem(int ProductId, int Quantity);

public record class BasketProduct(int Id, string Name, decimal UnitPrice, int Quantity)
{
    public decimal TotalPrice { get; } = UnitPrice * Quantity;
}
