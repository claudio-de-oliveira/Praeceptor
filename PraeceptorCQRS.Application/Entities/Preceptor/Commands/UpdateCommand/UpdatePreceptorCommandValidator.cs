using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Commands
{
    public class UpdatePreceptorCommandValidator : AbstractValidator<UpdatePreceptorCommand>
    {
        public UpdatePreceptorCommandValidator(
            IPreceptorRepository preceptorRepository,
            IPreceptorDegreeTypeRepository preceptorDegreeTypeRepository,
            IPreceptorRegimeTypeRepository preceptorRegimeTypeRepository)
        {
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
        }
    }
}

