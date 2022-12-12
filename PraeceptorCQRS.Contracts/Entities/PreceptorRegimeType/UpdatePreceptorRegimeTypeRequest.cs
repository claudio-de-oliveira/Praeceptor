namespace PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType
{
    public record UpdatePreceptorRegimeTypeRequest(
        Guid Id,
        string Code,
        string Code3
    );
}

