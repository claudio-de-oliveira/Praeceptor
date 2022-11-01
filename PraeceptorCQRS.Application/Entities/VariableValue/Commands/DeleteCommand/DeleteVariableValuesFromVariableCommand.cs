using ErrorOr;
using MediatR;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.DeleteCommand
{
    public record DeleteVariableValuesFromVariableCommand(
        Guid VariableId
        ) : IRequest<ErrorOr<bool>>;
}
