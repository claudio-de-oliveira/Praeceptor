using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class InsertNodeBeforeCommandValidator : AbstractValidator<InsertNodeBeforeCommand>
    {
        public InsertNodeBeforeCommandValidator()
        {
            RuleFor(o => o.FirstEntityId)
                .NotNull();
            RuleFor(o => o.SecondEntityId)
                .NotNull();
        }
    }
}
