using CommonPracticePatterns.OperationResult.RegistrationApplication.Entities;
using static CommonPracticePatterns.OperationResult.RegistrationApplication.Results.RegistrationServiceResult;

namespace CommonPracticePatterns.OperationResult.RegistrationApplication.Services;

public class ConcertRegistrationService
{
    public async Task<ConcertRegistrationResult> RegisterAsync(User user, Concert concert)
    {
        var (success, confirmationNumber) = await SimulatedRegistrationProcessAsync(user, concert);

        if (!success)
        {
            return ConcertRegistrationResult.CreateFailure(
                user,
                concert,
                "The registration to the concert failed.");
        }

        return ConcertRegistrationResult.CreateSuccess(
            user, concert, confirmationNumber);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "Added for example purposes.")]
    private static async Task<(bool Success, string ConfirmationNumber)> SimulatedRegistrationProcessAsync(User user, Concert concert)
    {
        // Simulate an async operation
        await Task.Delay(Random.Shared.Next(10, 100));

        // Return a simulated result
        return (concert.Id == 1, Guid.NewGuid().ToString());
    }
}
