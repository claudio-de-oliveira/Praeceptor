using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand
{
    public record DeleteVariableCommand(
        Guid Id
        ) : IRequest<ErrorOr<VariableResult>>;
}
