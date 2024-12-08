using DesignPatterns.Adapter.ExternalService;

namespace DesignPatterns.Adapter.Services;

public class ExternalGreeterAdapter(ExternalGreeter adaptee) : IGreeter
{
    public string Greeting() => adaptee.GreetByName(nameof(ExternalGreeterAdapter));
}
