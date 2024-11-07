namespace DesignPatterns.Strategy.CustomCollections.SortStrategy;

public interface ISortStrategy<T>
{
    IOrderedEnumerable<T> Sort(IEnumerable<T> items);
}
