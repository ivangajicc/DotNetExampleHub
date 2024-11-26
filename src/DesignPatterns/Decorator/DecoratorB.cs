using DesignPatterns.Decorator.Interfaces;

namespace DesignPatterns.Decorator;

public class DecoratorB(IComponent component) : IComponent
{
    public string Operation()
    {
        var result = component.Operation();
        return $"<DecoratorB>{result}</DecoratorB>";
    }
}
