namespace PraeceptorCQRS.Contracts.Entities.SubSection
{
    public record CreateSubSectionRequest(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
    );
}

