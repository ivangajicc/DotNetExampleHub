namespace BackendForFrontend.WebApi;

public interface ICurrentCustomerService
{
    int Id { get; }
}

public class FakeCurrentCustomerService : ICurrentCustomerService
{
    public int Id => 1;
}
