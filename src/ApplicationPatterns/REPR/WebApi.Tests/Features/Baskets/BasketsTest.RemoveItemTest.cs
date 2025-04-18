using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using static REPR.WebApi.Features.Baskets.Baskets;

namespace REPR.WebApi.Tests.Features.Baskets;

public partial class BasketsTest
{
    public class RemoveItemTest
    {
        [Fact]
        public async Task Should_remove_the_specified_item_from_the_customer_cart()
        {
            // Arrange
            await using var application = new WebApiApp();
            await application.SeedAsync<BasketContext>(async db =>
            {
                db.Items.RemoveRange(db.Items.ToArray());
                db.Items.Add(new BasketItem(1, 3, 30));
                db.Items.Add(new BasketItem(2, 1, 5));
                db.Items.Add(new BasketItem(2, 3, 15));
                db.Items.Add(new BasketItem(3, 2, 18));
                await db.SaveChangesAsync();
            });
            using var client = application.CreateClient();

            // Act
            using var response = await client.DeleteAsync("/baskets/2/1");

            // Assert the response
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
            var result = await response.Content.ReadFromJsonAsync<RemoveItem.Response>();
            Assert.NotNull(result);
            Assert.Equal(5, result.Quantity);

            // Assert the database state
            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider.GetRequiredService<BasketContext>();
            var dbItem = db.Items.FirstOrDefault(x => x.CustomerId == 2 && x.ProductId == 1);
            Assert.Null(dbItem);
            var remainingItems = db.Items.Count();
            Assert.Equal(3, remainingItems);
        }

        [Fact]
        public async Task Should_return_a_ProblemDetails_with_a_NotFound_status_code()
        {
            // Arrange
            await using var application = new WebApiApp();
            using var client = application.CreateClient();

            // Act
            using var response = await client.DeleteAsync("/baskets/99/99");

            // Assert the response
            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal("The product \u002799\u0027 is not in your shopping cart.", problem.Title);

            // Assert the database state
            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider.GetRequiredService<BasketContext>();
            var dbItem = db.Items.FirstOrDefault(x => x.CustomerId == 99);
            Assert.Null(dbItem);
        }
    }
}
