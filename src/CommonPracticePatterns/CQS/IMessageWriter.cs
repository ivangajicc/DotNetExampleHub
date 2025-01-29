namespace CommonPracticePatterns.CQS;

public interface IMessageWriter
{
    void Write(IChatRoom chatRoom, ChatMessage message);
}
