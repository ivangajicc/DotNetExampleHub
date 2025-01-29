using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPracticePatterns.CQS;

public interface IParticipant
{
    string Name { get; }

    void Join(IChatRoom chatRoom);

    void Leave(IChatRoom chatRoom);

    void SendMessageTo(IChatRoom chatRoom, string message);

    void NewMessageReceivedFrom(IChatRoom chatRoom, ChatMessage message);

    IEnumerable<IParticipant> ListParticipantsOf(IChatRoom chatRoom);

    IEnumerable<ChatMessage> ListMessagesOf(IChatRoom chatRoom);
}

public interface IChatRoom
{
    string Name { get; }

    void Add(IParticipant participant);

    void Add(ChatMessage message);

    void Remove(IParticipant participant);

    IEnumerable<IParticipant> ListParticipants();

    IEnumerable<ChatMessage> ListMessages();
}
