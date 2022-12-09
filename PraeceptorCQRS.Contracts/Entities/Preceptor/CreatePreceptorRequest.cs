namespace PraeceptorCQRS.Contracts.Entities.Preceptor;

public record CreatePreceptorRequest(
    string Code,
    string Name,
    string Email,
    string? Image,
    Guid DegreeTypeId,
    Guid RegimeTypeId,
    Guid InstituteId
);

