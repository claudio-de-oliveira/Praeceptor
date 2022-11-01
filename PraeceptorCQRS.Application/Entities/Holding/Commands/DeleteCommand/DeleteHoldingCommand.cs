using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Holding.Common;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public record DeleteHoldingCommand(
        Guid Id
        ) : IRequest<ErrorOr<HoldingResult>>;
}

