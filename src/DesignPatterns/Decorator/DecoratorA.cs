using DesignPatterns.Decorator.Interfaces;

namespace DesignPatterns.Decorator;

public class DecoratorA(IComponent component) : IComponent
{
    public string Operation()
    {
        var result = component.Operation();
        return $"<DecoratorA>{result}</DecoratorA>";
    }
}
