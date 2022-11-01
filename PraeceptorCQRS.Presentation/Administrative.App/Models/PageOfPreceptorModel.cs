namespace Administrative.App.Models
{
    public record PageOfPreceptorModel(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<PreceptorModel> Entities
        );
}
