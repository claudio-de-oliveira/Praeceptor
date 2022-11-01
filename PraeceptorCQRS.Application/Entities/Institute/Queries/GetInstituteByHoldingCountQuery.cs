using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public record GetInstituteByHoldingCountQuery(
        Guid HoldingId
        ) : IRequest<ErrorOr<InstituteCountResult>>;
}
