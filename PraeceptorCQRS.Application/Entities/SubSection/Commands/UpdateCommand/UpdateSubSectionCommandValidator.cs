using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public class UpdateSubSectionCommandValidator : AbstractValidator<UpdateSubSectionCommand>
    {
        public UpdateSubSectionCommandValidator()
        {
            RuleFor(o => o.Title)
                .NotNull()
                .MaximumLength(1024);
        }
    }
}

