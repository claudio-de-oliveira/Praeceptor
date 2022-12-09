using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries
{
    public record GetPreceptorRoleTypeByCodeQuery(
        string Code,
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorRoleTypeResult>>;
}
