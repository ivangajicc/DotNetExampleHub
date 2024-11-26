using DesignPatterns.DecoratorUsingScrutor.Interfaces;

namespace DesignPatterns.DecoratorUsingScrutor;

public class DecoratorA(IComponent component) : IComponent
{
    public string Operation()
    {
        var result = component.Operation();
        return $"<DecoratorA>{result}</DecoratorA>";
    }
}
