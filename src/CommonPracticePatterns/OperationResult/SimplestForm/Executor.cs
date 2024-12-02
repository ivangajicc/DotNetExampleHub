namespace CommonPracticePatterns.OperationResult.SimplestForm;

public class Executor
{
    public OperationResult Operation()
    {
        var randomNumber = Random.Shared.Next(100);

        var success = randomNumber % 2 == 0;

        return new OperationResult(success);
    }

    public record OperationResult(bool Succeeded);
}
