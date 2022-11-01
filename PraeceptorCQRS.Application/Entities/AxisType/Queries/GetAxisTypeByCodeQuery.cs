using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public record GetAxisTypeByCodeQuery(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<AxisTypeResult>>;
}
