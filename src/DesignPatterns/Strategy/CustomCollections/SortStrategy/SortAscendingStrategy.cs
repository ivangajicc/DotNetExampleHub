namespace DesignPatterns.Strategy.CustomCollections.SortStrategy;

public class SortAscendingStrategy<T> : ISortStrategy<T>
    where T : IComparable<T>
{
    public IOrderedEnumerable<T> Sort(IEnumerable<T> items) => items.OrderBy(x => x);
}
