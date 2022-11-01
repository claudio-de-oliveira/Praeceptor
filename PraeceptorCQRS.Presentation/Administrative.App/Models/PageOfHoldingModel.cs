namespace Administrative.App.Models
{
    public record PageOfHoldingModel(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<HoldingModel> Entities
        );
}
