namespace PraeceptorCQRS.Contracts.Entities.Holding
{
    public record HoldingResponse(
        Guid Id,

        string Acronym,
        string Name,
        string? Address,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

