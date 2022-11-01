using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public class CreateChapterCommandValidator : AbstractValidator<CreateChapterCommand>
    {
        public CreateChapterCommandValidator(IInstituteRepository repository)
        {
            // TODO: add CreateChapterCommand validation rules here
            RuleFor(o => o.Title)
                .NotEmpty()
                .MaximumLength(1024);

            // institute must exist
            RuleFor(x => x.InstituteId)
                .MustAsync(async (instituteId, cancellation) =>
                {
                    bool exists = await repository.Exists(o => o.Id == instituteId);
                    return exists;
                });
        }
    }
}

