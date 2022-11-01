namespace PraeceptorCQRS.Contracts.Entities.Course
{
    public record CreateCourseRequest(
        string Code,
        string Name,
        Guid? CEO,
        int AC,
        int NumberOfSeasons,
        int MinimumWorkload,
        string? Telephone,
        string? Email,
        string? Image,
        Guid InstituteId
    );
}
