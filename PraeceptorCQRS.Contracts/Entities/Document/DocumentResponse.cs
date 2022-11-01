namespace PraeceptorCQRS.Contracts.Entities.Document
{
    public record DocumentResponse(
        Guid Id,

        string Title,
        string? Text,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

