using FluentValidation;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateVariableCommandValidator 
        : AbstractValidator<CreateVariableCommand>
    {
        public CreateVariableCommandValidator(IGroupRepository groupRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20);

            // group must exist
            RuleFor(x => x.GroupId)
                .MustAsync(async (groupId, cancellation) =>
                {
                    bool exists = await groupRepository.Exists(o => o.Id == groupId);
                    return exists;
                });
        }
    }
}
