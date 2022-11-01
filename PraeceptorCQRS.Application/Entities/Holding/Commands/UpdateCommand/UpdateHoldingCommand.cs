using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Holding.Common;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public record UpdateHoldingCommand(
        Guid Id,
        string Name,
        string? Address
        ) : IRequest<ErrorOr<HoldingResult>>;
}

