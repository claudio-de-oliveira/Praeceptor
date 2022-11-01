namespace PraeceptorCQRS.Contracts.Entities.Preceptor
{
    public record PreceptorResponse(
        Guid Id,

        string Code,
        string Name,
        string Email,
        string? Image,
        Guid DegreeTypeId,
        Guid RegimeTypeId,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

