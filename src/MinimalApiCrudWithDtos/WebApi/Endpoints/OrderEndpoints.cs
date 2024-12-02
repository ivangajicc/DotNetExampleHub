using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApiCrudWithDtos.Application.Dtos;
using MinimalApiCrudWithDtos.Application.FakePersistance;
using MinimalApiCrudWithDtos.Application.Models;

namespace MinimalApiCrudWithDtos.WebApi.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/orders").WithTags("Order Endpoints");

        group.MapGet("/", GetOrdersSummaryAsync);
        group.MapGet("/{orderId}", GetOrderDetailsAsync);
        group.MapPut("/{orderId}", UpdateOrderAsync);
        group.MapPost("/", CreateOrderAsync);
        group.MapDelete("/{orderId}", DeleteOrderAsync);

        return group;
    }

    private static async Task<Ok<IEnumerable<OrderSummaryDto>>> GetOrdersSummaryAsync(
        IOrderRepository orderRepository,
        CancellationToken cancellationToken)
    {
        var orders = await orderRepository.AllAsync(cancellationToken);

        var ordersSummary = orders.Select(order => new OrderSummaryDto(
            OrderId: order.OrderId,
            CustomerName: order.CustomerName,
            TotalNumberOfItems: order.Items.Count,
            NumberOfPendingItems: order.Items
                .Count(x => x.Status.State != DeliveryState.Delivered)));

        return TypedResults.Ok(ordersSummary);
    }

    private static async Task<Results<Ok<OrderDetailsDto>, NotFound>> GetOrderDetailsAsync(
        int orderId,
        IOrderRepository orderRepository,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindAsync(orderId, cancellationToken);
        if (order == null)
        {
            return TypedResults.NotFound();
        }

        var dto = MapOrderToOrderDetails(order);

        return TypedResults.Ok(dto);
    }

    private static async Task<Results<
        Ok<OrderDetailsDto>,
        NotFound,
        Conflict
    >> UpdateOrderAsync(
            int orderId,
            UpdateOrderDto input,
            IOrderRepository orderRepository,
            CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindAsync(
            orderId,
            cancellationToken);
        if (order == null)
        {
            return TypedResults.NotFound();
        }

        var updatedOrder = await orderRepository.UpdateAsync(
            order with { CustomerName = input.CustomerName },
            cancellationToken);
        if (updatedOrder == null)
        {
            return TypedResults.Conflict();
        }

        var dto = MapOrderToOrderDetails(updatedOrder);

        return TypedResults.Ok(dto);
    }

    private static async Task<Results<Created<OrderDetailsDto>, NotFound>> CreateOrderAsync(
        CreateOrderDto input,
        IOrderRepository orderRepository,
        CancellationToken cancellationToken)
    {
        var createdOrder = await orderRepository.CreateAsync(
            new(0, input.CustomerName, new()),
            cancellationToken);

        var dto = MapOrderToOrderDetails(createdOrder);

        return TypedResults.Created($"/dto/orders/{createdOrder.OrderId}", dto);
    }

    private static async Task<Results<Ok<OrderDetailsDto>, NotFound, Conflict>> DeleteOrderAsync(
        int orderId,
        IOrderRepository orderRepository,
        CancellationToken cancellationToken)
    {
        var deletedOrder = await orderRepository.DeleteAsync(orderId, cancellationToken);
        if (deletedOrder == null)
        {
            return TypedResults.NotFound();
        }

        var dto = MapOrderToOrderDetails(deletedOrder);

        return TypedResults.Ok(dto);
    }

    private static OrderDetailsDto MapOrderToOrderDetails(Order order)
    {
        var dto = new OrderDetailsDto(
            OrderId: order.OrderId,
            CustomerName: order.CustomerName,
            Items: order.Items.Select(item => new OrderItemDetailsDto(
                ItemId: item.ItemId,
                ItemName: item.ItemName,
                ItemDescription: item.ItemDescription,
                QuantityTotal: item.Status.TotalQuantity,
                QuantityDelivered: item.Status.QuantityDelivered,
                DeliveryState: item.Status.State.ToString(),
                SupplierContactName: item.SupplierInfo.SupplierName,
                SupplierContactPerson: item.SupplierInfo.ContactPerson,
                SupplierContactEmail: item.SupplierInfo.ContactEmail)));
        return dto;
    }
}
