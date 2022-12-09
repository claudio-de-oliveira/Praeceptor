namespace Document.App.Models
{
    public record PageOfDocx(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<DocxModel> Entities
        );
}