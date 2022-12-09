using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands
{
    public record DeletePreceptorRoleTypeCommand(
        Guid Id
        ) : IRequest<ErrorOr<PreceptorRoleTypeResult>>;
}

