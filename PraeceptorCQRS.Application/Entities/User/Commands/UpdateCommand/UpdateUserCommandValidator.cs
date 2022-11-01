using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Commands.UpdateCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IApplicationUserRepository repository)
        {
            RuleFor(o => o.Id)
                .NotNull();
            RuleFor(o => o.UserName)
                .NotNull();
            RuleFor(o => o.Email)
                .NotNull();
            RuleFor(o => o.PasswordHash)
                .NotNull();
        }
    }
}
