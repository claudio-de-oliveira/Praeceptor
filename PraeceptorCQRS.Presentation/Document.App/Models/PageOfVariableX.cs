namespace Document.App.Models
{
    public record PageOfVariableX(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<VariableXModel> Entities
        );
}
