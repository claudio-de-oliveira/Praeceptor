namespace PraeceptorCQRS.Contracts.Entities.Institute
{
    public record UpdateInstituteRequest(
        Guid Id,
        string Name,
        string? Address
    );
}

