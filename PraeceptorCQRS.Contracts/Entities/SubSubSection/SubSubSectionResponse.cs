namespace PraeceptorCQRS.Contracts.Entities.SubSubSection
{
    public record SubSubSectionResponse(
        Guid Id,

        // TODO: replace this comment with other SubSubSection fields
        string Title,
        string? Text,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

