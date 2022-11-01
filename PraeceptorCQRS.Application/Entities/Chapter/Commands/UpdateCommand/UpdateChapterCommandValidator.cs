using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public class UpdateChapterCommandValidator : AbstractValidator<UpdateChapterCommand>
    {
        public UpdateChapterCommandValidator()
        {

            // TODO: add the UpdateChapterCommand validation rules here
            RuleFor(o => o.Title)
                .NotEmpty()
                .MaximumLength(1024);
        }
    }
}

