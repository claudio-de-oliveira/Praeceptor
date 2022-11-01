using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Authentication.Common;

namespace PraeceptorCQRS.Application.Authentication.Queries
{
    public record LoginQuery(
        string UserName,
        string Password
        ) : IRequest<ErrorOr<AuthenticationResult>>;
}

