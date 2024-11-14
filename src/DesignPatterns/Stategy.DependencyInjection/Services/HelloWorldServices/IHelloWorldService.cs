namespace DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

public interface IHelloWorldService
{
    public string Language { get; }

    public string GenerateHelloWorldMessage();
}
