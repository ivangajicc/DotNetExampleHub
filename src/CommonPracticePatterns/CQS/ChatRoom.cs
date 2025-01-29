using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonPracticePatterns.CQS.Mediator;

namespace CommonPracticePatterns.CQS;

public class ChatRoom : IChatRoom
{
    private readonly List<IParticipant> _participants = new();
    private readonly List<ChatMessage> _chatMessages = new();

    public ChatRoom(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

    public string Name { get; }

    public void Add(IParticipant participant) => _participants.Add(participant);

    public void Add(ChatMessage message) => _chatMessages.Add(message);

    public IEnumerable<ChatMessage> ListMessages() => _chatMessages.AsReadOnly();

    public IEnumerable<IParticipant> ListParticipants() => _participants.AsReadOnly();

    public void Remove(IParticipant participant) => _participants.Remove(participant);
}
