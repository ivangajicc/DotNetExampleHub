using DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

namespace Microsoft.Extensions.DependencyInjection;

public static class HelloWorldServices
{
    public static void AddHelloWorldServices(this IServiceCollection services)
    {
        services.AddScoped<IHelloWorldService, EnglishHelloWorldService>();
        services.AddScoped<IHelloWorldService, FrenchHelloWorldService>();
        services.AddScoped<IHelloWorldService, GermanHelloWorldService>();
        services.AddScoped<HelloWorldServiceProvider>();
    }
}
