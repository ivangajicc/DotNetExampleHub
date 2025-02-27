using ForEvolve.ExceptionMapper;

namespace REPR.WebApi.Features.Baskets;

public class DuplicateBasketItemException : ConflictException
{
    public DuplicateBasketItemException(int productId)
        : base($"The product '{productId}' is already in your shopping cart.")
    {
    }

    public DuplicateBasketItemException()
        : base()
    {
    }

    public DuplicateBasketItemException(string message)
        : base(message)
    {
    }

    public DuplicateBasketItemException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
