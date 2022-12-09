namespace PraeceptorCQRS.Contracts.Entities.SocialBody
{
    public record GetSocialBodyPageRequest(
        Guid CourseId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? NameFilter,
        Guid? DegreeFilter,
        Guid? RegimeFilter,
        Guid? RoleFilter
        );
}
