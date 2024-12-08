using DesignPatterns.Facade.TransparentFacadeSubSystem.Abstractions;

namespace DesignPatterns.Facade.TransparentFacadeSubSystem;

// Subsystem: Shipping
public class ShippingService : IShippingService
{
    public void ScheduleShipping(int orderId)
    {
        // Logic to schedule shipping
    }
}
