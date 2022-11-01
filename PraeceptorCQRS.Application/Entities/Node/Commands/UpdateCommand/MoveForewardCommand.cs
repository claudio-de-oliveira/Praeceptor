using ErrorOr;

using MediatR;

namespace PraeceptorCQRS.Application.Entities.Node.Commands.UpdateCommand
{
    public record MoveForwardCommand(
        Guid ParentId,
        Guid Id
        ) : IRequest<ErrorOr<bool>>;
}
