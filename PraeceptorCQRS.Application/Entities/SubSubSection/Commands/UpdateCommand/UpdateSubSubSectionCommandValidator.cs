using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public class UpdateSubSubSectionCommandValidator : AbstractValidator<UpdateSubSubSectionCommand>
    {
        public UpdateSubSubSectionCommandValidator()
        {
            RuleFor(o => o.Title)
                .NotNull()
                .MaximumLength(1024);
        }
    }
}

