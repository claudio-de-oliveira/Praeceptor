namespace Document.App.Models
{
    public record PageOfGroup(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<GroupModel> Entities
        );
}