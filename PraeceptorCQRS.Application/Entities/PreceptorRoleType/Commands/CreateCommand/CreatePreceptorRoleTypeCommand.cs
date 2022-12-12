using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands
{
    public record CreatePreceptorRoleTypeCommand(
        string Code,
        string Code3,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorRoleTypeResult>>;
}

