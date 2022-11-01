using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public record CreateVariableCommand(
        string Code,
        Guid GroupId
        ) : IRequest<ErrorOr<VariableResult>>;
}
