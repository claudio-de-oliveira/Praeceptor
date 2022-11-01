using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class CreateFirstNodeCommandValidator : AbstractValidator<CreateFirstNodeCommand>
    {
        public CreateFirstNodeCommandValidator(IListRepository repository)
        {
            RuleFor(o => o.FirstEntityId)
                .NotNull();
            RuleFor(o => o.SecondEntityId)
                .NotNull();
            RuleFor(x => x.FirstEntityId)
                .MustAsync(async (firstEntityId, cancellation) =>
                {
                    bool exists = await repository.Exists(o => o.FirstEntityId == firstEntityId);
                    return !exists;
                });

        }
    }
}
