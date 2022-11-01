namespace Administrative.App.Models
{
    public record PageOfAxisTypeModel(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<AxisTypeModel> Entities
        );
}
