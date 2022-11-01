namespace PraeceptorCQRS.Contracts.Entities.Admin
{
    public record CreateAdminRequest(
        string UserName,
        string Email,
        char Gender,
        string? PhoneNumber,
        string Id,
        string Role
        );
}
