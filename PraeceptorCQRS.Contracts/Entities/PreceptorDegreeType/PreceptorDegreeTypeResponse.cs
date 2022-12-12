namespace PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType
{
    public record PreceptorDegreeTypeResponse(
        Guid Id,

        string Code,
        string Code3,
        bool LatoSensu,
        bool StrictoSensu,

        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}