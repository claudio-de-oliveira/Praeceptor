namespace PraeceptorCQRS.Contracts.Entities.SubSection
{
    public record SubSectionResponse(
        Guid Id,

        // TODO: replace this comment with other SubSection fields
        string Title,
        string? Text,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

