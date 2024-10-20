using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Integration.Tests.Controllers;

public class WeatherForecastControllerTests(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> WebApplicationFactory { get; } = webApplicationFactory;

    private static readonly JsonSerializerOptions JsonSerializerOptions = CreateJsonSerializerOptions();

    public class Get(WebApplicationFactory<Program> webApplicationFactory)
        : WeatherForecastControllerTests(webApplicationFactory)
    {
        [Fact]
        public async Task Should_Return200()
        {
            using var httpClient = WebApplicationFactory.CreateClient();

            using var result = await httpClient.GetAsync("WeatherForecast");

            result.Should().Be200Ok();
        }

        [Fact]
        public async Task Should_RespondWithForecastData()
        {
            using var httpClient = WebApplicationFactory.CreateClient();

            using var result = await httpClient.GetAsync("WeatherForecast");

            var responseContent = await result.Content.ReadAsStringAsync();
            var forecastData =
                JsonSerializer.Deserialize<IReadOnlyCollection<WeatherForecast>>(responseContent, JsonSerializerOptions);

            forecastData.Should().HaveCount(3);
            forecastData.Should()
                .SatisfyRespectively(
                    first =>
                    {
                        first.Date.Should().Be(DateOnly.FromDateTime(DateTime.Now.AddDays(1)));
                    },
                    second =>
                    {
                        second.Date.Should().Be(DateOnly.FromDateTime(DateTime.Now.AddDays(2)));
                    },
                    third =>
                    {
                        third.Date.Should().Be(DateOnly.FromDateTime(DateTime.Now.AddDays(3)));
                    });
        }
    }

    private static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, AllowTrailingCommas = true,
        };

        return options;
    }
}
