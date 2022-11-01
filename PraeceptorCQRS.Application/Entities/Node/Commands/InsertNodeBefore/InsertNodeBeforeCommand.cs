using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public record InsertNodeBeforeCommand(
        Guid Id,
        Guid FirstEntityId,
        Guid DocumentId,
        Guid SecondEntityId
        ) : IRequest<ErrorOr<NodeResult>>;
}
