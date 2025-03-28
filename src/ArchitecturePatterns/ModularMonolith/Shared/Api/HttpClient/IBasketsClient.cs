using Baskets.Contracts;
using Refit;

namespace API.HttpClient;

public interface IBasketsClient
{
    [Get("/baskets/{query.CustomerId}")]
    Task<IEnumerable<FetchItemsResponseItem>> FetchCustomerBasketAsync(
        FetchItemsQuery query,
        CancellationToken cancellationToken);

    [Post("/baskets")]
    Task<AddItemResponse> AddProductToCartAsync(
        AddItemCommand command,
        CancellationToken cancellationToken);

    [Delete("/baskets/{command.CustomerId}/{command.ProductId}")]
    Task<RemoveItemResponse> RemoveProductFromCartAsync(
        RemoveItemCommand command,
        CancellationToken cancellationToken);

    [Put("/baskets")]
    Task<UpdateQuantityResponse> UpdateProductQuantityAsync(
        UpdateQuantityCommand command,
        CancellationToken cancellationToken);
}
