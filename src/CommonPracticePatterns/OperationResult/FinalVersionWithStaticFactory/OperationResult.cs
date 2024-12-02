using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace CommonPracticePatterns.OperationResult.FinalVersionWithStaticFactory;

public abstract record OperationResult
{
    private OperationResult()
    {
    }

    public abstract bool Succeeded { get; }

    public abstract string ResultType { get; }

    public static OperationResult Success(int? value = null) => new SuccessfulOperationResult { Value = value };

    public static OperationResult Failure(params OperationResultMessage[] errors) => new FailedOperationResult(errors);

    private record SuccessfulOperationResult : OperationResult
    {
        public override string ResultType => nameof(SuccessfulOperationResult);

        public override bool Succeeded { get; } = true;

        public virtual int? Value { get; init; }
    }

    // This can be extended to more domain driven results like - UnitOutOfOrderResult
    private sealed record FailedOperationResult : OperationResult
    {
        public FailedOperationResult(params OperationResultMessage[] errors) => Messages = [.. errors];

        public override string ResultType => nameof(FailedOperationResult);

        public override bool Succeeded { get; } = false;

        public ImmutableList<OperationResultMessage> Messages { get; }
    }
}
