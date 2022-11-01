namespace PraeceptorCQRS.Contracts.Authentication
{
    public record RegisterRequest(
        string UserName,
        string Email,
        string PasswordHash,
        char Gender,
        string HoldingId,
        string InstituteId,
        string CourseId,
        bool IsEnabled
    );
}

