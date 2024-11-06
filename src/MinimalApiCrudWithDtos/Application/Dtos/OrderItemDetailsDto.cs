using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiCrudWithDtos.Application.Dtos;

public record class OrderItemDetailsDto(
    int ItemId,
    string ItemName,
    string ItemDescription,
    int QuantityTotal,
    int QuantityDelivered,
    string DeliveryState,
    string SupplierContactName,
    string SupplierContactPerson,
    string SupplierContactEmail);
