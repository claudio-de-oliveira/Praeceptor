using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public record GetHoldingByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<HoldingResult>>;
}

