using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.User.Common;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public record GetUserByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<UserResult>>;
}
