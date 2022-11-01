using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public class CreatePreceptorDegreeTypeCommandValidator : AbstractValidator<CreatePreceptorDegreeTypeCommand>
    {
        public CreatePreceptorDegreeTypeCommandValidator(IPreceptorDegreeTypeRepository preceptorDegreeTypeRepository, IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20)
                // code must be unique
                .MustAsync(async (code, cancellation) =>
                {
                    bool exists = await preceptorDegreeTypeRepository.Exists(o => o.Code == code);
                    return !exists;
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

