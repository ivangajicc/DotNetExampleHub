using System.Collections.Immutable;
using MinimalApiCrudWithDtos.Application.Models;

namespace MinimalApiCrudWithDtos.Application.FakePersistance;

// Presentation only - Do not use static classes for save purposes please :)
public class OrderRepository : IOrderRepository
{
    public Task<Order?> FindAsync(int orderId, CancellationToken cancellationToken)
    {
        var entity = MemoryDataStore.Orders.Find(x => x.OrderId == orderId);
        return Task.FromResult(entity);
    }

    public Task<IEnumerable<Order>> AllAsync(CancellationToken cancellationToken)
    {
        var entities = MemoryDataStore.Orders.ToImmutableArray().AsEnumerable();
        return Task.FromResult(entities);
    }

    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var lastOrderId = FindLastOrderId();
        var lastItemId = FindLastItemId();
        var items = order.Items
            .ConvertAll(item => item with
            {
                ItemId = ++lastItemId,
            });

        var newOrder = order with
        {
            OrderId = lastOrderId + 1,
            Items = items,
        };

        MemoryDataStore.Orders.Add(newOrder);
        return Task.FromResult(newOrder);
    }

    public Task<Order?> UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        var index = MemoryDataStore.Orders.FindIndex(x => x.OrderId == order.OrderId);
        if (index == -1)
        {
            return Task.FromResult(default(Order));
        }

        MemoryDataStore.Orders[index] = order;
        return Task.FromResult<Order?>(order);
    }

    public Task<Order?> DeleteAsync(int orderId, CancellationToken cancellationToken)
    {
        var index = MemoryDataStore.Orders.FindIndex(x => x.OrderId == orderId);
        if (index == -1)
        {
            return Task.FromResult(default(Order));
        }

        var order = MemoryDataStore.Orders[index];
        MemoryDataStore.Orders.RemoveAt(index);
        return Task.FromResult<Order?>(order);
    }

    private int FindLastOrderId()
    {
        if (MemoryDataStore.Orders.Count == 0)
        {
            return 0;
        }

        return MemoryDataStore.Orders.Max(x => x.OrderId);
    }

    private int FindLastItemId()
    {
        if (MemoryDataStore.Orders.Count == 0)
        {
            return 0;
        }

        var items = MemoryDataStore.Orders.SelectMany(o => o.Items).Select(x => x.ItemId);

        if (!items.Any())
        {
            return 0;
        }

        return items.Max();
    }
}
