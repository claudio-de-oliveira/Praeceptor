using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public record CreateFirstNodeCommand(
        Guid FirstEntityId,
        Guid DocumentId,
        Guid SecondEntityId
        ) : IRequest<ErrorOr<NodeResult>>;
}
