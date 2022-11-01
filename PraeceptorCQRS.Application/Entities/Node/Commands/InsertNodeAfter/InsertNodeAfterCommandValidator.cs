using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class InsertNodeAfterCommandValidator : AbstractValidator<InsertNodeAfterCommand>
    {
        public InsertNodeAfterCommandValidator()
        {
            RuleFor(o => o.FirstEntityId)
                .NotNull();
            RuleFor(o => o.SecondEntityId)
                .NotNull();
        }
    }
}
