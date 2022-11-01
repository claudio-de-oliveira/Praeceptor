using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries
{
    public record GetPreceptorDegreeTypeByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorDegreeTypeCountResult>>;
}
