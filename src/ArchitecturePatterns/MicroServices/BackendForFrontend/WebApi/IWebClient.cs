using Baskets.WebApi.Features.Baskets;
using Refit;
using BasketsImport = Baskets.WebApi.Features.Baskets.Baskets;
using ProductsImport = Products.WebApi.Features.Products.Products;

namespace BackendForFrontend.WebApi;

public interface IWebClient
{
    IBasketsClient Baskets { get; }

    IProductsClient Catalog { get; }
}

public class DefaultWebClient : IWebClient
{
    public DefaultWebClient(IBasketsClient baskets, IProductsClient catalog)
    {
        Baskets = baskets ?? throw new ArgumentNullException(nameof(baskets));
        Catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
    }

    public IBasketsClient Baskets { get; }

    public IProductsClient Catalog { get; }
}

// We can leverage ApiResponse<T> also for more control over errors.
public interface IBasketsClient
{
    [Get("/baskets/{query.CustomerId}")]
    Task<IEnumerable<BasketsImport.FetchItems.Item>> FetchCustomerBasketAsync(
        BasketsImport.FetchItems.Query query,
        CancellationToken cancellationToken);

    [Post("/baskets")]
    Task<BasketsImport.AddItem.Response> AddProductToCartAsync(
        BasketsImport.AddItem.Command command,
        CancellationToken cancellationToken);

    [Delete("/baskets/{command.CustomerId}/{command.ProductId}")]
    Task<BasketsImport.RemoveItem.Response> RemoveProductFromCartAsync(
        BasketsImport.RemoveItem.Command command,
        CancellationToken cancellationToken);

    [Put("/baskets")]
    Task<BasketsImport.UpdateQuantity.Response> UpdateProductQuantityAsync(
        BasketsImport.UpdateQuantity.Command command,
        CancellationToken cancellationToken);
}

public interface IProductsClient
{
    [Get("/products/{query.ProductId}")]
    Task<ProductsImport.FetchOne.Response> FetchProductAsync(
        ProductsImport.FetchOne.Query query,
        CancellationToken cancellationToken);

    [Get("/products")]
    Task<ProductsImport.FetchAll.Response> FetchProductsAsync(
        CancellationToken cancellationToken);
}
