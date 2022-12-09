namespace PraeceptorCQRS.Contracts.Entities.Document
{
    public record DocumentTextResponse(
        Guid Id,
        string Title,
        string? Text
        );
}