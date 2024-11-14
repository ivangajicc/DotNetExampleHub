namespace DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

public class GermanHelloWorldService : IHelloWorldService
{
    public string Language => "DE";

    public string GenerateHelloWorldMessage() => "Hallo Welt";
}
