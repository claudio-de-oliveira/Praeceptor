using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;

namespace PraeceptorCQRS.Application.Entities.Pea.Command.UpdateCommand
{
    public record UpdatePeaCommand(
        Guid Id,
        string Text,
        string? LastModifiedBy
        ) : IRequest<ErrorOr<PeaResult>>;
}