using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Email)
                .MaximumLength(250)
                .EmailAddress().When(o => !string.IsNullOrWhiteSpace(o.Email));
        }
    }
}

