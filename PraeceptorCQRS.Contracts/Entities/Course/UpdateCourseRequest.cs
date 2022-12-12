namespace PraeceptorCQRS.Contracts.Entities.Course
{
    public record UpdateCourseRequest(
        Guid Id,
        string Name,
        int AC,
        int NumberOfSeasons,
        int MinimumWorkload,
        string? Telephone,
        string? Email,
        string? Image
        );
}