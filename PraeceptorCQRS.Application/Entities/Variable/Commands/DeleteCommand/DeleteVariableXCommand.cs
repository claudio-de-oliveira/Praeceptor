using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand
{
    public record DeleteVariableXCommand(
        Guid Id
        ) : IRequest<ErrorOr<VariableResultX>>;
}
