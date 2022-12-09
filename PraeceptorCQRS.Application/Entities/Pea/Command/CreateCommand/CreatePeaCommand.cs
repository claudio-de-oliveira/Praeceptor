using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;

namespace PraeceptorCQRS.Application.Entities.Pea.Command
{
    public record class CreatePeaCommand(
        Guid ClassId,
        string Text,
        string? CreatedBy
        ) : IRequest<ErrorOr<PeaResult>>;
}