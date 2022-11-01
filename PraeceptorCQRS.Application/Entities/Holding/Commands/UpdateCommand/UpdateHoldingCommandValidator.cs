using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public class UpdateHoldingCommandValidator : AbstractValidator<UpdateHoldingCommand>
    {
        public UpdateHoldingCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Address)
                .MaximumLength(4000);
        }
    }
}

