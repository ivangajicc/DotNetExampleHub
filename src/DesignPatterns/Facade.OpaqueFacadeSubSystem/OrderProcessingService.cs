using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Facade.OpaqueFacadeSubSystem;

internal class OrderProcessingService
{
    public int CreateOrder(string productId, int quantity) =>
        123; // Returns a mock order ID

    public string GetOrderStatus(int orderId) =>
        "Order Shipped"; // Simplified for example
}
