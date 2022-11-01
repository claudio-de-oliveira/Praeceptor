namespace Document.App.Models
{
    public record PageOfFile(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<FileModel> Entities
        );
}
