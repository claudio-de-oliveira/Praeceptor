using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public class CreateInstituteCommandValidator : AbstractValidator<CreateInstituteCommand>
    {
        public CreateInstituteCommandValidator(IInstituteRepository instituteRepository, IHoldingRepository holdingRepository)
        {
            RuleFor(x => x.Acronym)
                .NotEmpty()
                .MaximumLength(20)
                // code must be unique
                .MustAsync(async (acronym, cancellation) =>
                {
                    bool exists = await instituteRepository.Exists(o => o.Acronym == acronym);
                    return !exists;
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Address)
                .MaximumLength(4000);

            // holding must exist
            RuleFor(x => x.HoldingId)
                .MustAsync(async (holdingId, cancellation) =>
                {
                    bool exists = await holdingRepository.Exists(o => o.Id == holdingId);
                    return exists;
                });
        }
    }
}

