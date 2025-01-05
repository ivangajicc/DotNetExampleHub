namespace DesignPatterns.ChainOfRespnsibility;

public interface IMessageHandler
{
    void Handle(Message message);
}
