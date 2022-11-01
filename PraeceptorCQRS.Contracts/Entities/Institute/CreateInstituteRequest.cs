namespace PraeceptorCQRS.Contracts.Entities.Institute
{
    public record CreateInstituteRequest(
        string Acronym,
        string Name,
        string? Address,
        Guid HoldingId
    );
}

