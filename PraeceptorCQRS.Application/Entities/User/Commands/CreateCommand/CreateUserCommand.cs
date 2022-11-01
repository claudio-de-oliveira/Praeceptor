using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.User.Common;

namespace PraeceptorCQRS.Application.Entities.User.Commands.CreateCommand
{
    public record CreateUserCommand(
        string UserName,
        string Email,
        string PasswordHash,
        string? PhoneNumber,
        bool IsEnabled,
        char Gender,
        string? HoldingId,
        string? InstituteId,
        string? CourseId
        ) : IRequest<ErrorOr<UserResult>>;
}
