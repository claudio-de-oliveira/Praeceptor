using FluentValidation;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.CreateCommand
{
    public class CreateVariableValueCommandValidator : AbstractValidator<CreateVariableValueCommand>
    {
        public CreateVariableValueCommandValidator(IVariableRepository variableRepository, IGroupValueRepository groupValueRepository)
        {
            // group value must exist
            RuleFor(x => x.GroupValueId)
                .MustAsync(async (groupValueId, cancellation) =>
                {
                    bool exists = await groupValueRepository.Exists(o => o.Id == groupValueId);
                    return exists;
                });
            // variable must exist
            RuleFor(x => x.VariableId)
                .MustAsync(async (variableId, cancellation) =>
                {
                    bool exists = await variableRepository.Exists(o => o.Id == variableId);
                    return exists;
                });
        }
    }
}
