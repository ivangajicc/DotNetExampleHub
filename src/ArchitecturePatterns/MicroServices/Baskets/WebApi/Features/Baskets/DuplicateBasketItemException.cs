using ForEvolve.ExceptionMapper;

namespace Baskets.WebApi.Features.Baskets;

public class DuplicateBasketItemException : ConflictException
{
    public DuplicateBasketItemException(int productId)
        : base($"The product '{productId}' is already in your shopping cart.")
    {
    }
}
