using DesignPatterns.DecoratorUsingScrutor.Interfaces;

namespace DesignPatterns.DecoratorUsingScrutor;

public class DecoratorB(IComponent component) : IComponent
{
    public string Operation()
    {
        var result = component.Operation();
        return $"<DecoratorB>{result}</DecoratorB>";
    }
}
