using System.Collections;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace REPR.WebApi.Features.Baskets;

public partial class Baskets
{
    public partial class FetchItems
    {
        public record Query(int CustomerId);

        public record Response(IEnumerable<Item> Items) : IEnumerable<Item>
        {
            public IEnumerator<Item> GetEnumerator()
                => Items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => ((IEnumerable)Items).GetEnumerator();
        }

        public record class Item(int ProductId, int Quantity);

        [Mapper]
        public partial class Mapper
        {
            public partial Response Map(IQueryable<BasketItem> items);
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator() => RuleFor(x => x.CustomerId).GreaterThan(0);
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

            public async Task<Response> HandleAsync(Query query, CancellationToken cancellationToken)
            {
                var items = _db.Items.Where(x => x.CustomerId == query.CustomerId);
                await items.LoadAsync(cancellationToken);
                var result = _mapper.Map(items);
                return result;
            }
        }
    }

    public static IServiceCollection AddFetchItems(this IServiceCollection services) => services
            .AddScoped<FetchItems.Handler>()
            .AddSingleton<FetchItems.Mapper>()
;

    public static IEndpointRouteBuilder MapFetchItems(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/{CustomerId}",
            ([AsParameters] FetchItems.Query query, FetchItems.Handler handler, CancellationToken cancellationToken)
                => handler.HandleAsync(query, cancellationToken)
        );
        return endpoints;
    }
}
