
using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Node.Common;

namespace PraeceptorCQRS.Application.Entities.Node.Queries
{
    public record GetNodeByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<NodeResult>>;
}
