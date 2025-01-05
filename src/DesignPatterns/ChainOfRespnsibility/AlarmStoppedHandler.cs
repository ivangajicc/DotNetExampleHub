using DesignPatterns.ChainOfRespnsibility.Properties;

namespace DesignPatterns.ChainOfRespnsibility;

public class AlarmStoppedHandler : MessageHandlerBase
{
    public AlarmStoppedHandler(IMessageHandler? next = null)
        : base(next)
    {
    }

    protected override string HandledMessageName => "AlarmStopped";

    protected override void Process(Message message)
    {
        // Do some logic.
    }
}
