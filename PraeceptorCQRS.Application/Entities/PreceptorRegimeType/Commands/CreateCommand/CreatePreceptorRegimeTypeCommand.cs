using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Domain.Values;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public record CreatePreceptorRegimeTypeCommand(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorRegimeTypeResult>>;
}

