using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace CommonPracticePatterns.OperationResult.FinalVersionWithStaticFactory;

public class OperationResultMessage
{
    public OperationResultMessage(string message, OperationResultSeverity severity)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Severity = severity;
    }

    public string Message { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OperationResultSeverity Severity { get; }
}
