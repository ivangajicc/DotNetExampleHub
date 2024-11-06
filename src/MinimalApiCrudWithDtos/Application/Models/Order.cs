using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiCrudWithDtos.Application.Models;

public record class Order(
    int OrderId,
    string CustomerName,
    List<OrderItem> Items);
