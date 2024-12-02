using System.Diagnostics.CodeAnalysis;
using CommonPracticePatterns.OperationResult.RegistrationApplication.Entities;

namespace CommonPracticePatterns.OperationResult.RegistrationApplication.Results;

public static class RegistrationServiceResult
{
    public record class ConcertRegistrationResult
    {
        private ConcertRegistrationResult()
        {
        }

        [MemberNotNullWhen(false, nameof(ErrorMessage))]
        [MemberNotNullWhen(true, nameof(ConfirmationNumber))]
        public bool RegistrationSucceeded { get; init; }

        public User User { get; init; } = null!;

        public Concert Concert { get; init; } = null!;

        public string? ConfirmationNumber { get; init; }

        public string? ErrorMessage { get; init; }

        public static ConcertRegistrationResult CreateSuccess(User user, Concert concert, string confirmationNumber) => new()
        {
            RegistrationSucceeded = true,
            User = user,
            Concert = concert,
            ConfirmationNumber = confirmationNumber,
        };

        public static ConcertRegistrationResult CreateFailure(User user, Concert concert, string errorMessage) => new()
        {
            RegistrationSucceeded = false,
            User = user,
            Concert = concert,
            ErrorMessage = errorMessage,
        };
    }
}
