namespace DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

public class EnglishHelloWorldService : IHelloWorldService
{
    public string Language => "EN";

    public string GenerateHelloWorldMessage() => "Hello World";
}
