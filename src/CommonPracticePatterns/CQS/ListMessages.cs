using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPracticePatterns.CQS.Mediator;

namespace CommonPracticePatterns.CQS;

public static class ListMessages
{
    public record class Query(IChatRoom ChatRoom) :
        IQuery<IEnumerable<ChatMessage>>;

    public class Handler : IQueryHandler<Query, IEnumerable<ChatMessage>>
    {
        public IEnumerable<ChatMessage> Handle(Query query) => query.ChatRoom.ListMessages();
    }
}
