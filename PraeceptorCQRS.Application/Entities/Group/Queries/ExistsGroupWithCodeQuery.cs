using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Group.Common;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public record ExistsGroupWithCodeQuery(
        Guid InstituteId,
        string Code
        ) : IRequest<ErrorOr<GroupExistResult>>;
}
