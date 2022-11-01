using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public record GetGroupsByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<GroupsCountResult>>;
}
