namespace PraeceptorCQRS.Contracts.Entities.SubSection
{
    public record UpdateSubSectionRequest(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
    );
}

