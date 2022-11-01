using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public record GetGroupByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<GroupResult>>;
}
