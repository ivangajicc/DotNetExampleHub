using System.Collections.Immutable;
using static CommonPracticePatterns.OperationResult.WithSeverity.SeverityOperationResult;

namespace CommonPracticePatterns.OperationResult.WithSeverity;

public record class OperationResult
{
    public OperationResult() => Messages = [];

    public OperationResult(params OperationResultMessage[] messages) => Messages = [.. messages];

    public bool Succeeded => !HasErrors();

    public int? Value { get; init; }

    public ImmutableList<OperationResultMessage> Messages { get; init; }

    public bool HasErrors() => FindErrors().Any();

    private IEnumerable<OperationResultMessage> FindErrors()
        => Messages.Where(x => x.Severity == OperationResultSeverity.Error);
}

public record class OperationResultMessage
{
    public OperationResultMessage(string message, OperationResultSeverity severity)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Severity = severity;
    }

    public string Message { get; }

    public OperationResultSeverity Severity { get; }
}
