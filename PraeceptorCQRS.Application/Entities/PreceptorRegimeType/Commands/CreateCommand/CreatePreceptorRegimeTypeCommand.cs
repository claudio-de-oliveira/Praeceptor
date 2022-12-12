using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public record CreatePreceptorRegimeTypeCommand(
        string Code,
        string Code3,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorRegimeTypeResult>>;
}

