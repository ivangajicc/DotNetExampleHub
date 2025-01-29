using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPracticePatterns.CQS.Mediator;

namespace CommonPracticePatterns.CQS;

public static class ListParticipants
{
    public record class Query(IChatRoom ChatRoom) :
        IQuery<IEnumerable<IParticipant>>;

    public class Handler : IQueryHandler<Query, IEnumerable<IParticipant>>
    {
        public IEnumerable<IParticipant> Handle(Query query) => query.ChatRoom.ListParticipants();
    }
}
