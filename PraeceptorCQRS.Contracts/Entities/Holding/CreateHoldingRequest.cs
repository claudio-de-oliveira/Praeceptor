namespace PraeceptorCQRS.Contracts.Entities.Holding
{
    public record CreateHoldingRequest(
        string Acronym,
        string Name,
        string? Address
    );
}

