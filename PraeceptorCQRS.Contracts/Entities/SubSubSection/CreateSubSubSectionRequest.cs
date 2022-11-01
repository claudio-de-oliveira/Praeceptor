namespace PraeceptorCQRS.Contracts.Entities.SubSubSection
{
    public record CreateSubSubSectionRequest(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
    );
}

