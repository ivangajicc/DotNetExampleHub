using MinimalApiCrudWithDtos.Application.Models;

namespace MinimalApiCrudWithDtos.Application.FakePersistance;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> AllAsync(CancellationToken cancellationToken);

    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);

    Task<Order?> DeleteAsync(int orderId, CancellationToken cancellationToken);

    Task<Order?> FindAsync(int orderId, CancellationToken cancellationToken);

    Task<Order?> UpdateAsync(Order order, CancellationToken cancellationToken);
}
