namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record PreceptorDegreeTypeResponse(
        Guid Id,

        string Code,
        bool LatoSensu,
        bool StrictoSensu,

        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}