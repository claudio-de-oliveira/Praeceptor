using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.User.Common;

namespace PraeceptorCQRS.Application.Entities.User.Commands.DeleteCommand
{
    public record DeleteUserCommand(
        Guid Id
        ) : IRequest<ErrorOr<UserResult>>;
}
