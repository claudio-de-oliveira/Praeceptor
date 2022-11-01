namespace PraeceptorCQRS.Contracts.Entities.Institute
{
    public record InstituteResponse(
        Guid Id,
        string Acronym,
        string Name,
        string? Address,
        Guid HoldingId,
        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

