using DesignPatterns.Facade;
using DesignPatterns.Facade.OpaqueFacadeSubSystem;
using DesignPatterns.Facade.OpaqueFacadeSubSystem.Abstractions;
using DesignPatterns.Facade.TransparentFacadeSubSystem;
using DesignPatterns.Facade.TransparentFacadeSubSystem.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<IInventoryService, UpdatedInventoryService>()
    .AddOpaqueFacadeSubSystem()
    .AddTransparentFacadeSubSystem();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost(
    "/opaque/PlaceOrder",
    (PlaceOrder order, IECommerceOpaqueFacade eCommerceOpaqueFacade)
        => eCommerceOpaqueFacade.PlaceOrder(order.ProductId, order.Quantity));
app.MapGet(
    "/opaque/CheckOrderStatus/{orderId}",
    (int orderId, IECommerceOpaqueFacade eCommerceOpaqueFacade)
        => eCommerceOpaqueFacade.CheckOrderStatus(orderId));

app.MapPost(
    "/transparent/PlaceOrder",
    (PlaceOrder order, IECommerceTransparentFacade eCommerceTransparentFacade)
        => eCommerceTransparentFacade.PlaceOrder(order.ProductId, order.Quantity));
app.MapGet(
    "/transparent/CheckOrderStatus/{orderId}",
    (int orderId, IECommerceTransparentFacade eCommerceTransparentFacade)
        => eCommerceTransparentFacade.CheckOrderStatus(orderId));

app.Run();

public record PlaceOrder(string ProductId, int Quantity);
