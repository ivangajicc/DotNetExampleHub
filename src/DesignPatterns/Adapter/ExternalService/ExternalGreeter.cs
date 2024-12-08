namespace DesignPatterns.Adapter.ExternalService;

public class ExternalGreeter
{
    public string GreetByName(string name) => $"Adaptee says: hi {name}!";
}
