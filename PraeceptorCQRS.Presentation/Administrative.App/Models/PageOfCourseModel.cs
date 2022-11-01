namespace Administrative.App.Models
{
    public record PageOfCourseModel(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<CourseModel> Entities
        );
}
