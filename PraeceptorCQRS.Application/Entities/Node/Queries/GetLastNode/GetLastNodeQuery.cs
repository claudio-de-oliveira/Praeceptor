
using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Node.Common;

namespace PraeceptorCQRS.Application.Entities.Node.Queries
{
    public record GetLastNodeQuery(
        Guid FirstEntityId
        ) : IRequest<ErrorOr<NodeResult>>;
}
