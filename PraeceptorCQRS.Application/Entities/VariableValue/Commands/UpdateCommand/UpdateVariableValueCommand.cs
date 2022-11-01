using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.UpdateCommand
{
    public record UpdateVariableValueCommand(
        Guid Id,
        string Value
        ) : IRequest<ErrorOr<VariableValueResult>>;
}
