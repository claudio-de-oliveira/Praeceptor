using ErrorOr;
using MediatR;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand
{
    public record DeleteVariablesFromGroupCommand(
        Guid GroupId
        ) : IRequest<ErrorOr<bool>>;
}
