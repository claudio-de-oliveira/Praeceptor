using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public record UpdatePreceptorRegimeTypeCommand(
        Guid Id,
        string Code,
        string Code3
        ) : IRequest<ErrorOr<PreceptorRegimeTypeResult>>;
}

