using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateVariableXByHoldingCommandValidator : AbstractValidator<CreateVariableXByHoldingCommand>
    {
        public CreateVariableXByHoldingCommandValidator(IVariableXRepository variableRepository, IHoldingRepository holdingRepository)
        {
            RuleFor(x => x.GroupName)
                .NotEmpty();
            RuleFor(x => x.VariableName)
                .NotEmpty();
            // holding must exist
            RuleFor(x => x.GroupId)
                .MustAsync(async (holdingId, cancellation) =>
                {
                    bool exists = await holdingRepository.Exists(o => o.Id == holdingId);
                    return exists;
                });
        }
    }
}
