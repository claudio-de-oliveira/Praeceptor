using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries
{
    public record GetPreceptorDegreeTypeByCodeQuery(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorDegreeTypeResult>>;
}
