using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Holding.Common;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public record CreateHoldingCommand(
        string Acronym,
        string Name,
        string? Address
        ) : IRequest<ErrorOr<HoldingResult>>;
}

