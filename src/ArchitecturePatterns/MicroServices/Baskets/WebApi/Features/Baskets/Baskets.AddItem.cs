using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace Baskets.WebApi.Features.Baskets;

public partial class Baskets
{
    public partial class AddItem
    {
        public record class Command(
            int CustomerId,
            int ProductId,
            int Quantity);

        public record class Response(
            int ProductId,
            int Quantity);

        [Mapper]
        public partial class Mapper
        {
            public partial BasketItem Map(Command item);

            public partial Response Map(BasketItem item);
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.CustomerId).GreaterThan(0);
                RuleFor(x => x.ProductId).GreaterThan(0);
                RuleFor(x => x.Quantity).GreaterThan(0);
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
                var itemExists = await _db.Items.AnyAsync(
                    x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
                    cancellationToken: cancellationToken);
                if (itemExists)
                {
                    throw new DuplicateBasketItemException(command.ProductId);
                }

                var item = _mapper.Map(command);
                _db.Add(item);
                await _db.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map(item);
                return result;
            }
        }
    }

    public static IServiceCollection AddAddItem(this IServiceCollection services) => services
            .AddScoped<AddItem.Handler>()
            .AddSingleton<AddItem.Mapper>()
        ;

    public static IEndpointRouteBuilder MapAddItem(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/",
            async (AddItem.Command command, AddItem.Handler handler, CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(command, cancellationToken);
                return TypedResults.Created($"/products/{result.ProductId}", result);
            });
        return endpoints;
    }
}
