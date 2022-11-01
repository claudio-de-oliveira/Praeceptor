using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class UpdateNodeCommandValidator : AbstractValidator<UpdateNodeCommand>
    {
        public UpdateNodeCommandValidator()
        {
            RuleFor(o => o.SecondEntityId)
                .NotNull();
        }
    }
}
