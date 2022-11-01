using FluentValidation;

namespace PraeceptorCQRS.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Password)
                .NotNull();
            RuleFor(x => x.UserName)
                .NotNull();
        }
    }
}
