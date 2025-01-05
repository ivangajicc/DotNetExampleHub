namespace DesignPatterns.TemplateMethod;

public abstract class SearchMachine
{
    protected SearchMachine(params int[] values)
        => Values = values ?? throw new ArgumentNullException(nameof(values));

    protected int[] Values { get; }

    public int? IndexOf(int value)
    {
        if (Values.Length == 0)
        {
            return null;
        }

        var result = Find(value);

        return result;
    }

    protected abstract int? Find(int value);
}
