namespace DesignPatterns.Strategy.DependencyInjection.Services.HelloWorldServices;

public class HelloWorldServiceProvider(IEnumerable<IHelloWorldService> helloWorldServices)
{
    public IHelloWorldService GetHelloWorldService(string language)
        => helloWorldServices.FirstOrDefault(s => s.Language.Equals(language, StringComparison.OrdinalIgnoreCase)) ??
           helloWorldServices.First(x => x.Language.Equals("EN", StringComparison.OrdinalIgnoreCase));
}
