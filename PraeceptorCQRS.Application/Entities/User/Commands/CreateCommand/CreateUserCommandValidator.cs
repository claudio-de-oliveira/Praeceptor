using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Commands.CreateCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IApplicationUserRepository repository)
        {
            RuleFor(o => o.UserName)
                .NotNull();
            RuleFor(o => o.Email)
                .NotNull();
            RuleFor(o => o.PasswordHash)
                .NotNull();
        }
    }
}
