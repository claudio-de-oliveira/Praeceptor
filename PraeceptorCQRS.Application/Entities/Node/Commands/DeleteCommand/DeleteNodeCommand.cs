using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Node.Common;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public record DeleteNodeCommand(
        Guid Id
        ) : IRequest<ErrorOr<NodeResult>>;
}
