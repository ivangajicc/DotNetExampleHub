namespace DesignPatterns.TemplateMethod;

public class BinarySearchMachine : SearchMachine
{
    public BinarySearchMachine(params int[] values)
        : base([.. values.OrderBy(v => v)])
    {
    }

    protected override int? Find(int value)
    {
        var index = Array.BinarySearch(Values, value);

        return index < 0 ? null : index;
    }
}
