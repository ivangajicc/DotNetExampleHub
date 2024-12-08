using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DesignPatterns.Composite.Models;

public abstract class BookComposite(string name) : IComponent
{
    private readonly List<IComponent> _children = new();

    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

    public virtual string Type => GetType().Name;

    public virtual int Count => _children.Sum(child => child.Count);

    public virtual IEnumerable Children => new ReadOnlyCollection<IComponent>(_children);

    public virtual void Add(IComponent bookComponent) => _children.Add(bookComponent);

    public virtual void Remove(IComponent bookComponent) => _children.Remove(bookComponent);
}
