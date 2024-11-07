namespace DesignPatterns.Strategy.CustomCollections.SortStrategy;

public class SortDescendingStrategy<T> : ISortStrategy<T>
    where T : IComparable<T>
{
    public IOrderedEnumerable<T> Sort(IEnumerable<T> items) => items.OrderByDescending(x => x);
}
