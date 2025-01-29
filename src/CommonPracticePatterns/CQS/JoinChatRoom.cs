using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonPracticePatterns.CQS.Mediator;

namespace CommonPracticePatterns.CQS;

public static class JoinChatRoom
{
    public record class Command(IChatRoom ChatRoom, IParticipant Requester) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        public void Handle(Command command) => command.ChatRoom.Add(command.Requester);
    }
}
