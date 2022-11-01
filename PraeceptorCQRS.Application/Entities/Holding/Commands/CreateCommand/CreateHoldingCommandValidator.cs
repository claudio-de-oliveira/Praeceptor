using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public class CreateHoldingCommandValidator : AbstractValidator<CreateHoldingCommand>
    {
        private readonly IHoldingRepository _repository;

        public CreateHoldingCommandValidator(IHoldingRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Acronym)
                .NotEmpty()
                .MaximumLength(20)
                .MustAsync(async (acronym, cancellation) =>
                {
                    bool exists = await _repository.Exists(o => o.Acronym == acronym);
                    return !exists;
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Address)
                .MaximumLength(4000);
        }
    }
}

