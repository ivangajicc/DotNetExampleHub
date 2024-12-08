namespace DesignPatterns.Composite.Models;

public class Book(string title) : IComponent
{
    public string Title { get; } = title ?? throw new ArgumentNullException(nameof(title));

    public string Type => "Book";

    public int Count { get; } = 1;
}
