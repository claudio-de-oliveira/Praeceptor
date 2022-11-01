namespace PraeceptorCQRS.Contracts.Entities.Chapter
{
    public record ChapterResponse(
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

