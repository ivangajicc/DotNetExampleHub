using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiCrudWithDtos.Application.Dtos;

public record class OrderDetailsDto(
    int OrderId,
    string CustomerName,
    IEnumerable<OrderItemDetailsDto> Items);
