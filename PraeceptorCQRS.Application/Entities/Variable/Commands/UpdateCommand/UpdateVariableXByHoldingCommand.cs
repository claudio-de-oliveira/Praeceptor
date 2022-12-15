using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.UpdateCommand
{
    public record UpdateVariableXByHoldingCommand(
        Guid Id,
        string? Value
        ) : IRequest<ErrorOr<VariableResultX>>;
}
