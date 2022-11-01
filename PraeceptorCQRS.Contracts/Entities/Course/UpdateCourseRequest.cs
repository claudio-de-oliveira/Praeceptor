namespace PraeceptorCQRS.Contracts.Entities.Course
{
    public record UpdateCourseRequest(
        Guid Id,
        string Name,
        Guid? CEO,
        int AC,
        int NumberOfSeasons,
        int MinimumWorkload,
        string? Telephone,
        string? Email,
        string? Image
    );
}

