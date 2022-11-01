namespace PraeceptorCQRS.Contracts.Entities.Document
{
    public record CreateDocumentRequest(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
    );
}

