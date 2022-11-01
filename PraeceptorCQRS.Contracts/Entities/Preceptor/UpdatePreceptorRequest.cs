namespace PraeceptorCQRS.Contracts.Entities.Preceptor
{
    public record UpdatePreceptorRequest(
        Guid Id,

        string Name,
        string Email,
        string? Image,
        Guid DegreeTypeId,
        Guid RegimeTypeId
    );
}

