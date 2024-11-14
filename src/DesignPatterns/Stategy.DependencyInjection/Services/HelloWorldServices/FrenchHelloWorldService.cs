namespace DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

public class FrenchHelloWorldService : IHelloWorldService
{
    public string Language => "FR";

    public string GenerateHelloWorldMessage() => "Bonjour le monde";
}
