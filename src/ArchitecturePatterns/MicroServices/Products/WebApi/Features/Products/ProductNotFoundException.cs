using ForEvolve.ExceptionMapper;

namespace Products.WebApi.Features.Products;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int productId)
        : base($"The product '{productId}' was not found.")
    {
    }
}
