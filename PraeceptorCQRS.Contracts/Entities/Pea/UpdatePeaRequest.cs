namespace PraeceptorCQRS.Contracts.Entities.Pea
{
    public record UpdatePeaRequest(
        Guid Id,
        string Text,
        string? LastModifiedBy
        );
}