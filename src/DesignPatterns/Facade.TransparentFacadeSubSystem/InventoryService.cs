using DesignPatterns.Facade.TransparentFacadeSubSystem.Abstractions;

namespace DesignPatterns.Facade.TransparentFacadeSubSystem;

// Subsystem: Inventory
public class InventoryService : IInventoryService
{
    public bool CheckStock(string productId, int quantity) =>
        true; // Simplified for example
}
