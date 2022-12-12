namespace PraeceptorCQRS.Contracts.Entities.Course
{
    public record GetCoursePageRequest(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? NameFilter,
        int ACFilter,
        int SeasonsFilter,
        int MinimumWorkloadFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        );
}
