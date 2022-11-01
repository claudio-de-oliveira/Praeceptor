
using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Node.Common;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public record UpdateNodeCommand(
        Guid Id,
        Guid SecondEntityId
        ) : IRequest<ErrorOr<NodeResult>>;
}
