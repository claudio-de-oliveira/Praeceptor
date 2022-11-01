namespace PraeceptorCQRS.Contracts.Entities.Chapter
{
    public record UpdateChapterRequest(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
    );
}

