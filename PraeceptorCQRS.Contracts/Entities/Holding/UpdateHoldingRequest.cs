namespace PraeceptorCQRS.Contracts.Entities.Holding
{
    public record UpdateHoldingRequest(
        Guid Id,

        string Name,
        string? Address
    );
}

