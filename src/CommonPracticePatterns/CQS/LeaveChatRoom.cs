using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPracticePatterns.CQS.Mediator;

namespace CommonPracticePatterns.CQS;

public static class LeaveChatRoom
{
    public record class Command(IChatRoom ChatRoom, IParticipant Requester) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        public void Handle(Command command) => command.ChatRoom.Remove(command.Requester);
    }
}
