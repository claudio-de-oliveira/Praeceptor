using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.CreateCommand
{
    public record CreateVariableValueCommand(
        Guid GroupValueId,
        Guid VariableId,
        string Value
        ) : IRequest<ErrorOr<VariableValueResult>>;
}
