using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Queries
{
    public record GetGroupValuesByGroupQuery(
        Guid GroupId
        ) : IRequest<ErrorOr<GroupValueListResult>>;
}
