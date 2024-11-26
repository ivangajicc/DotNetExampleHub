using Microsoft.AspNetCore.Mvc.Testing;

namespace DesignPatterns.DecoratorUsingScrutor.Integration.Tests;

public class GetTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Should_return_a_double_decorated_string()
    {
        // Arrange
        using var client = webApplicationFactory.CreateClient();

        // Act
        using var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal(
            "<DecoratorB><DecoratorA>Hello from ComponentA</DecoratorA></DecoratorB>",
            body);
    }
}
