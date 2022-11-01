using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public record GetAxisTypeByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<AxisTypeResult>>;
}
