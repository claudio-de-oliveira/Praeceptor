using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public record DeletePreceptorRegimeTypeCommand(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorRegimeTypeResult>>;
}

