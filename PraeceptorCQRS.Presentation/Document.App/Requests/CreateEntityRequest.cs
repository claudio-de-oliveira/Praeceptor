namespace Document.App.Requests
{
    public record CreateEntityRequest(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
    );
}
