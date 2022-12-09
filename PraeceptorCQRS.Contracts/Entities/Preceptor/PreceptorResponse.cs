using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;
using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;

namespace PraeceptorCQRS.Contracts.Entities.Preceptor
{
    public record PreceptorResponse(
        Guid Id,

        string Code,
        string Name,
        string Email,
        string? Image,
        PreceptorDegreeTypeResponse DegreeType,
        Guid DegreeTypeId,
        PreceptorRegimeTypeResponse RegimeType,
        Guid RegimeTypeId,
        Guid InstituteId,

        DateTime Created,
        string? CreatedBy,
        DateTime? LastModified,
        string? LastModifiedBy
   );
}

