using FluentValidation;

namespace PraeceptorCQRS.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty();
            RuleFor(x => x.PasswordHash)
                .NotEmpty();

        }
    }
}
