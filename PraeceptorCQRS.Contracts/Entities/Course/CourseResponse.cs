namespace PraeceptorCQRS.Contracts.Entities.Course
{
    public record CourseResponse(
        Guid Id,

        string Code,
        string Name,
        int AC,
        int NumberOfSeasons,
        int MinimumWorkload,
        string? Telephone,
        string? Email,
        string? Image,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

