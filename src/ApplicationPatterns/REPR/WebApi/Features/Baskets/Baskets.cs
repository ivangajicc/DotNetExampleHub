using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace REPR.WebApi.Features.Baskets;

public static partial class Baskets
{
    public record BasketItem(int CustomerId, int ProductId, int Quantity);

    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options)
            : base(options)
        {
        }

        public DbSet<BasketItem> Items => Set<BasketItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<BasketItem>()
                .HasKey(x => new { x.CustomerId, x.ProductId })
            ;
        }
    }

    public static IServiceCollection AddBasketsFeature(this IServiceCollection services) => services
            .AddAddItem()
            .AddFetchItems()
            .AddRemoveItem()
            .AddUpdateQuantity()
            .AddDbContext<BasketContext>(options => options
                .UseInMemoryDatabase("BasketContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            )
        ;

    public static IEndpointRouteBuilder MapBasketsFeature(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup(nameof(Baskets).ToLower(System.Globalization.CultureInfo.InvariantCulture))
            .WithTags(nameof(Baskets))
        ;
        group
            .MapFetchItems()
            .MapAddItem()
            .MapUpdateQuantity()
            .MapRemoveItem()
        ;
        return endpoints;
    }

    public static Task SeedBasketsAsync(this IServiceScope scope) => Task.CompletedTask;
}
