using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.DeleteCommand
{
    public record DeleteVariableValueCommand(
        Guid Id
        ) : IRequest<ErrorOr<VariableValueResult>>;
}
