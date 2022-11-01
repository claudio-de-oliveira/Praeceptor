namespace PraeceptorCQRS.Contracts.Entities.Chapter
{
    public record CreateChapterRequest(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
    );
}

