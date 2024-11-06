using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiCrudWithDtos.Application.Models;

public record class OrderItem(
    int ItemId,
    string ItemName,
    string ItemDescription,
    ItemStatus Status,
    SupplierContact SupplierInfo);
