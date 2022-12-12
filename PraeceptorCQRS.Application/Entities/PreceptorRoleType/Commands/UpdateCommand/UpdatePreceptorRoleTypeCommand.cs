using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands
{
    public record UpdatePreceptorRoleTypeCommand(
        Guid Id,
        string Code,
        string Code3
        ) : IRequest<ErrorOr<PreceptorRoleTypeResult>>;
}

