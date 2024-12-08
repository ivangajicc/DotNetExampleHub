using DesignPatterns.Facade.TransparentFacadeSubSystem.Abstractions;

namespace DesignPatterns.Facade.TransparentFacadeSubSystem;

// Subsystem: Order Processing
public class OrderProcessingService : IOrderProcessingService
{
    public int CreateOrder(string productId, int quantity) =>
        123; // Returns a mock order ID

    public string GetOrderStatus(int orderId) => "Order Shipped"; // Simplified for example
}
