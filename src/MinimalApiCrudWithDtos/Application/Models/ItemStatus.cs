using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiCrudWithDtos.Application.Models;

public record class ItemStatus(int TotalQuantity, int QuantityDelivered)
{
    public DeliveryState State => DecideDeliveryStateBasedOnQuantityDelivered();

    private DeliveryState DecideDeliveryStateBasedOnQuantityDelivered()
    {
        if (QuantityDelivered == 0)
        {
            return DeliveryState.Pending;
        }
        else if (QuantityDelivered == TotalQuantity)
        {
            return DeliveryState.Delivered;
        }

        return DeliveryState.Partial;
    }
}
