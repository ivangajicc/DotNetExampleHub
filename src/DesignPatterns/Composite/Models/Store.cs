namespace DesignPatterns.Composite.Models;

public class Store(string name, string location, string manager) : BookComposite(name)
{
    public string Location { get; } = location;

    public string Manager { get; } = manager;
}
