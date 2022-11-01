namespace PraeceptorCQRS.Contracts.Entities.User
{
    public record UpdateUserRequest(
        Guid Id,
        string UserName,
        string Email,
        string PasswordHash,
        string? PhoneNumber,
        bool IsEnabled,
        char Gender,
        string? HoldingId,
        string? InstituteId,
        string? CourseId
    );
}
