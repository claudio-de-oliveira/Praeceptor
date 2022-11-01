using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Document.Commands
{
    public class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentCommandValidator()
        {
            RuleFor(o => o.Title)
                .NotNull()
                .MaximumLength(1024);
        }
    }
}

