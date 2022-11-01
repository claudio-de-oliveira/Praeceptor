using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator(IClassRepository classRepository, IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20)
                // code must be unique
                .MustAsync(async (code, cancellation) =>
                {
                    bool exists = await classRepository.Exists(o => o.Code == code);
                    return !exists;
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            // institute must exist
            RuleFor(x => x.InstituteId)
                .MustAsync(async (instituteId, cancellation) =>
                {
                    bool exists = await instituteRepository.Exists(o => o.Id == instituteId);
                    return exists;
                });
        }
    }
}

