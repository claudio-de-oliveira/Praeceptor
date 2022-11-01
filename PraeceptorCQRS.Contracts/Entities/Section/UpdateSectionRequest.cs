namespace PraeceptorCQRS.Contracts.Entities.Section
{
    public record UpdateSectionRequest(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
    );
}

