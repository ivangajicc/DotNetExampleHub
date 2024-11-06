using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalApiCrudWithDtos.Application.Models;

namespace MinimalApiCrudWithDtos.Application.FakePersistance;

public static class MemoryDataStore
{
    public static List<Order> Orders { get; } = new();

    public static void Seed()
    {
        Orders.Add(new Order(
            OrderId: 1,
            CustomerName: "Ivan",
            Items: new List<OrderItem>
            {
                new OrderItem(
                    ItemId: 1,
                    ItemName: "Item 1",
                    ItemDescription: "Item 1 desc.",
                    Status: new ItemStatus(
                        TotalQuantity: 200,
                        QuantityDelivered: 100
                    ),
                    SupplierInfo: new SupplierContact(
                        SupplierName: "Easy transport",
                        ContactPerson: "Gajoni",
                        ContactEmail: "Gajoni@najgoriNaZemljinojKori.com")),
                new OrderItem(
                    ItemId: 1,
                    ItemName: "Item 2",
                    ItemDescription: "Item 2 desc.",
                    Status: new ItemStatus(
                        TotalQuantity: 333,
                        QuantityDelivered: 333
                    ),
                    SupplierInfo: new SupplierContact(
                        SupplierName: "Easy transport",
                        ContactPerson: "Gajoni",
                        ContactEmail: "Gajoni@najgoriNaZemljinojKori.com")),
            }));
        Orders.Add(new Order(
             OrderId: 2,
             CustomerName: "Milan",
             []));
    }
}
