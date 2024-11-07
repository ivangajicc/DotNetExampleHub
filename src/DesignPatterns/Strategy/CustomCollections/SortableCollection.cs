using System.Collections.Immutable;
using DesignPatterns.Strategy.CustomCollections.SortStrategy;

namespace DesignPatterns.Strategy.CustomCollections;

public class SortableCollection<T>(IEnumerable<T> items)
    where T : IComparable<T>
{
    private ISortStrategy<T> _sortStrategy = new SortAscendingStrategy<T>();

    private ImmutableArray<T> _items = items.ToImmutableArray();

    public IEnumerable<T> Items => _items;

    public void SetSortStrategy(ISortStrategy<T> strategy)
        => _sortStrategy = strategy;

    public void Sort() => _items = [.. _sortStrategy.Sort(Items)];
}
