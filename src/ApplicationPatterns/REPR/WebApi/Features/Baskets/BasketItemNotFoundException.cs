using ForEvolve.ExceptionMapper;

namespace REPR.WebApi.Features.Baskets;

public class BasketItemNotFoundException : NotFoundException
{
    public BasketItemNotFoundException(int productId)
        : base($"The product '{productId}' is not in your shopping cart.")
    {
    }

    public BasketItemNotFoundException()
        : base()
    {
    }

    public BasketItemNotFoundException(string message)
        : base(message)
    {
    }

    public BasketItemNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
