using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.User.Common;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public record GetUserByInstituteCountQuery(
        Guid InstituteId
        ) : IRequest<ErrorOr<UserCountResult>>;
}
