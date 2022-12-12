namespace PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;

public record CreatePreceptorRegimeTypeRequest(
    string Code,
    string Code3,
    Guid InstituteId
    );

