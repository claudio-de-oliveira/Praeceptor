using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;

namespace PraeceptorCQRS.Application.Entities.Pea.Command.DeleteCommand
{
    public record DeletePeaCommand(
        Guid ClassId
        ) : IRequest<ErrorOr<PeaResult>>;
}