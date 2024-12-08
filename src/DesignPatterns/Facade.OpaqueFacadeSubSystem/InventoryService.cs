using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Facade.OpaqueFacadeSubSystem;

// Subsystem: Inventory
internal class InventoryService
{
    public bool CheckStock(string productId, int quantity) =>
        true;
}
