using DesignPatterns.ChainOfRespnsibility.Properties;

namespace DesignPatterns.ChainOfRespnsibility;

public class AlarmPausedHandler : MessageHandlerBase
{
    public AlarmPausedHandler(IMessageHandler? next = null)
        : base(next)
    {
    }

    protected override string HandledMessageName => "AlarmPaused";

    protected override void Process(Message message)
    {
        // Do some logic.
    }
}
