namespace PraeceptorCQRS.Contracts.Entities.Document
{
    public record UpdateDocumentRequest(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
    );
}

