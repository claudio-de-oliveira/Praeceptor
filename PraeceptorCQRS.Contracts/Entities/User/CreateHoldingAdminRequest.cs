namespace PraeceptorCQRS.Contracts.Entities.User
{
    public record CreateHoldingAdminRequest(
        string UserName,
        string Email,
        string HoldingId,
        string HoldingAcronym,
        string HoldingName
        );
}
