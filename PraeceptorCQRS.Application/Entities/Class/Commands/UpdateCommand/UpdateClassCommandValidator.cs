using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}

