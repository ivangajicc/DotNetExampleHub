namespace DesignPatterns.Facade.OpaqueFacadeSubSystem.Abstractions;

public interface IECommerceOpaqueFacade
{
    string PlaceOrder(string productId, int quantity);

    string CheckOrderStatus(int orderId);
}
