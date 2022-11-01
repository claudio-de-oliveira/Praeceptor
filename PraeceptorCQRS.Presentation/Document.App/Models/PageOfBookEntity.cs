namespace Document.App.Models
{
    public record PageOfBookEntity(
        int CurrentPage,
        int Size,
        int PreviousPage,
        int NextPage,
        int NumberOfPages,
        List<BookEntity> Entities
        );
}
