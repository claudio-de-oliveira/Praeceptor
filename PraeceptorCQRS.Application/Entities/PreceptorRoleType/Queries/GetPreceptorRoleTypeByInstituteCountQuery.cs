using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries
{
    public record GetPreceptorRoleTypeByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<PreceptorRoleTypeCountResult>>;
}
