using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Preceptor.Common;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public record UpdatePreceptorCommand(
        Guid Id,
        string Name,
        string Email,
        string? Image,
        Guid DegreeTypeId,
        Guid RegimeTypeId
        ) : IRequest<ErrorOr<PreceptorResult>>;
}

