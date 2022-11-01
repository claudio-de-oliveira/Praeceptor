namespace PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType
{
    public record CreatePreceptorRegimeTypeRequest(
        string Code,
        Guid InstituteId
    );
}

