namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record PreceptorDegreeTypeResponse(
        Guid Id,

        string Code,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

