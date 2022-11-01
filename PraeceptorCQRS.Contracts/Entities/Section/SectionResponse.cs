namespace PraeceptorCQRS.Contracts.Entities.Section
{
    public record SectionResponse(
        Guid Id,

        // TODO: replace this comment with other Section fields
        string Title,
        string? Text,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

