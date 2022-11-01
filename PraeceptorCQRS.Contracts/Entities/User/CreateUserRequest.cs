namespace PraeceptorCQRS.Contracts.Entities.User
{
    public record CreateUserRequest(
        string UserName,
        string Email,
        string? PhoneNumber,
        bool IsEnabled,
        char Gender,
        string? HoldingId,
        string? InstituteId,
        string? CourseId
    );
}
