namespace PraeceptorCQRS.Contracts.Authentication
{
    public record AuthenticationResponse(
        Guid Id,
        string UserName,
        string NormalizedUserName,
        string Email,
        string NormalizedEmail,
        bool EmailConfirmed,
        string PasswordHash,
        string SecurityStamp,
        string ConcurrencyStamp,
        string PhoneNumber,
        bool PhoneNumberConfirmed,
        bool TwoFactorEnabled,
        DateTimeOffset? LockoutEnd,
        bool LockoutEnabled,
        int AccessFailedCount,

        bool IsEnabled,
        char Gender,
        string HoldingId,
        string InstituteId,
        string CourseId
    );
}

