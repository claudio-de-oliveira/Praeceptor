using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public record GetHoldingByCodeQuery(
        string Code
        ) : IRequest<ErrorOr<HoldingResult>>;
}
