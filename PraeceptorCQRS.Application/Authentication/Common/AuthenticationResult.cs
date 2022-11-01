using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Authentication.Common
{
    public record AuthenticationResult(
        ApplicationUser User,
        string Token
        );
}

