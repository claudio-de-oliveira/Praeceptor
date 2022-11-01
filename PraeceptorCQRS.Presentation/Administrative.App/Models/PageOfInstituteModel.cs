namespace Administrative.App.Models
{
    public record PageOfInstituteModel(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<InstituteModel> Entities
        );
}
