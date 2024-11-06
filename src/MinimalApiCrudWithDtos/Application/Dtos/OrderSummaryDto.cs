using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiCrudWithDtos.Application.Dtos;

public record class OrderSummaryDto(
    int OrderId,
    string CustomerName,
    int TotalNumberOfItems,
    int NumberOfPendingItems);
