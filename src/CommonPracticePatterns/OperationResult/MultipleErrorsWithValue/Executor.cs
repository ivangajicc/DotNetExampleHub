using System.Collections.Immutable;

namespace CommonPracticePatterns.OperationResult.MultipleErrorsWithValue;

public class Executor
{
    public OperationResult Operation()
    {
        // Randomize the success indicator
        // This should be real logic
        var randomNumber = Random.Shared.Next(100);
        var success = randomNumber % 2 == 0;

        // Return the operation result
        return success
            ? new() { Value = randomNumber }
            : new($"Something went wrong with the number '{randomNumber}'.")
            {
                Value = randomNumber,
            };
    }

    public record class OperationResult
    {
        public OperationResult() => Errors = [];

        public OperationResult(params string[] errors) => Errors = [.. errors];

        public bool Succeeded => !HasErrors();

        public int? Value { get; init; }

        public IReadOnlyCollection<string> Errors { get; init; }

        public bool HasErrors() => Errors?.Count > 0;
    }
}
