namespace DesignPatterns.Composite.Models;

public class Corporation(string name, string ceo) : BookComposite(name)
{
    public string CEO { get; } = ceo;
}
