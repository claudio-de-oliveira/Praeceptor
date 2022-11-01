using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Authentication.Common;

namespace PraeceptorCQRS.Application.Authentication.Commands
{
    public record RegisterCommand(
        string UserName,
        string Email,
        char Gender,
        string PhoneNumber,
        string PasswordHash,
        string HoldingId,
        string InstituteId,
        string CourseId
        ) : IRequest<ErrorOr<AuthenticationResult>>;
}

