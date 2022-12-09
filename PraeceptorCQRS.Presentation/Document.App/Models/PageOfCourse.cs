namespace Document.App.Models
{
    public record PageOfCourse(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<CourseModel> Entities
        );
}