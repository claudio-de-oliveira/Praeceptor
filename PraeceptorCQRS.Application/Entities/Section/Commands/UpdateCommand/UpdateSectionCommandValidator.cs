using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
    {
        public UpdateSectionCommandValidator()
        {
            RuleFor(o => o.Title)
                .NotNull()
                .MaximumLength(1024);
        }
    }
}

