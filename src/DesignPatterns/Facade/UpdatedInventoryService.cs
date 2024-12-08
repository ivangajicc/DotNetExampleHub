using DesignPatterns.Facade.TransparentFacadeSubSystem.Abstractions;

namespace DesignPatterns.Facade;

public class UpdatedInventoryService : IInventoryService
{
    public bool CheckStock(string productId, int quantity) => false; // Simplified for example
}
