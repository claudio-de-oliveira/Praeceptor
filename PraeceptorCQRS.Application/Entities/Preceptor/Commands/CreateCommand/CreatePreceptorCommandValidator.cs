using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public class CreatePreceptorCommandValidator : AbstractValidator<CreatePreceptorCommand>
    {
        public CreatePreceptorCommandValidator(
            IPreceptorRepository preceptorRepository,
            IPreceptorDegreeTypeRepository preceptorDegreeTypeRepository,
            IPreceptorRegimeTypeRepository preceptorRegimeTypeRepository,
            IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20)
                // code must be unique
                .MustAsync(async (code, cancellation) =>
                {
                    bool exists = await preceptorRepository.Exists(o => o.Code == code);
                    return !exists;
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Email)
                .MaximumLength(250)
                .EmailAddress().When(o => !string.IsNullOrWhiteSpace(o.Email));

            RuleFor(x => x.Image)
                .MaximumLength(2000);

            // regime must exist
            RuleFor(x => x.RegimeTypeId)
                .MustAsync(async (regimeId, cancellation) =>
                {
                    bool exists = await preceptorRegimeTypeRepository.Exists(o => o.Id == regimeId);
                    return exists;
                });
            // degree must exist
            RuleFor(x => x.DegreeTypeId)
                .MustAsync(async (degreeId, cancellation) =>
                {
                    bool exists = await preceptorDegreeTypeRepository.Exists(o => o.Id == degreeId);
                    return exists;
                });

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
