namespace PraeceptorCQRS.Contracts.Entities.SubSubSection
{
    public record UpdateSubSubSectionRequest(
        Guid Id,
        string Title,
        string? Text,
        string? UpdatedBy
    );
}

