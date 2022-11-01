namespace Document.App.Requests
{
    public record UpdateEntityRequest(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
    );
}
