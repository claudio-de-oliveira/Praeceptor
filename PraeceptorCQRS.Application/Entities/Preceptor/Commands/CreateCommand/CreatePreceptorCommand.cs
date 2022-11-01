using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Domain.Values;

using Quartz.Util;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public record CreatePreceptorCommand(
        string Code,
        string Name,
        string Email,
        string? Image,
        Guid DegreeTypeId,
        Guid RegimeTypeId,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorResult>>;
}

