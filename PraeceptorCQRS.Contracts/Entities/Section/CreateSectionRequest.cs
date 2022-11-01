namespace PraeceptorCQRS.Contracts.Entities.Section
{
    public record CreateSectionRequest(
        string Title,
        string? Text,
        Guid InstituteId,
        string? CreatedBy
    );
}

