namespace Document.App.Models
{
    public record PageOfVariable(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<VariableModel> Entities
        );
}
