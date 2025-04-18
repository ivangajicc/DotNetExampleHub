using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace Baskets.WebApi.Features.Baskets;

public partial class Baskets
{
    public partial class RemoveItem
    {
        public record class Command(int CustomerId, int ProductId);

        public record class Response(int ProductId, int Quantity);

        [Mapper]
        public partial class Mapper
        {
            public partial Response Map(BasketItem item);
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.CustomerId).GreaterThan(0);
                RuleFor(x => x.ProductId).GreaterThan(0);
            }
        }

        public class Handler
        {
            private readonly BasketContext _db;
            private readonly Mapper _mapper;

            public Handler(BasketContext db, Mapper mapper)
            {
                _db = db ?? throw new ArgumentNullException(nameof(db));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken)
            {
                var item = await _db.Items.FirstOrDefaultAsync(
                    x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
                    cancellationToken: cancellationToken);
                if (item is null)
                {
                    throw new BasketItemNotFoundException(command.ProductId);
                }

                _db.Items.Remove(item);
                await _db.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map(item);
                return result;
            }
        }
    }

    public static IServiceCollection AddRemoveItem(this IServiceCollection services) => services
            .AddScoped<RemoveItem.Handler>()
            .AddSingleton<RemoveItem.Mapper>()
        ;

    public static IEndpointRouteBuilder MapRemoveItem(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
            "/{customerId}/{productId}",
            ([AsParameters] RemoveItem.Command command, RemoveItem.Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(command, cancellationToken));
        return endpoints;
    }
}
