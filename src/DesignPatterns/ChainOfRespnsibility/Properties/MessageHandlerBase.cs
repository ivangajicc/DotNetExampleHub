using System.Diagnostics.CodeAnalysis;

namespace DesignPatterns.ChainOfRespnsibility.Properties;

// Here we can see in play combination of template method and chain of responsibility patterns.
public abstract class MessageHandlerBase : IMessageHandler
{
    private readonly IMessageHandler? _next;

    protected MessageHandlerBase(IMessageHandler? next = null) => _next = next;

    protected abstract string HandledMessageName { get; }

    public void Handle(Message message)
    {
        if (CanHandle(message))
        {
            Process(message);
        }
        else if (HasNext())
        {
            _next.Handle(message);
        }
    }

    protected virtual bool CanHandle(Message message) => message.Name == HandledMessageName;

    protected abstract void Process(Message message);

    [MemberNotNullWhen(true, nameof(_next))]
    private bool HasNext() => _next != null;
}
