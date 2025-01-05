using DesignPatterns.ChainOfRespnsibility.Properties;

namespace DesignPatterns.ChainOfRespnsibility;

public class AlarmTriggeredHandler : MessageHandlerBase
{
    public AlarmTriggeredHandler(IMessageHandler? next = null)
        : base(next)
    {
    }

    protected override string HandledMessageName => "AlarmTriggered";

    protected override void Process(Message message)
    {
        // Do some logic.
    }
}
