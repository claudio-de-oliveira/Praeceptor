using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidator(IInstituteRepository repository)
        {
            RuleFor(o => o.Title)
                .NotNull()
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

