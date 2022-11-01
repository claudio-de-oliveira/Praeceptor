using FluentValidation;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.UpdateCommand
{
    public class UpdateGroupValueCommandValidator : AbstractValidator<UpdateGroupValueCommand>
    {
        public UpdateGroupValueCommandValidator(IGroupRepository groupRepository)
        {
            // group must exist
            RuleFor(x => x.Id)
                .MustAsync(async (groupId, cancellation) =>
                {
                    bool exists = await groupRepository.Exists(o => o.Id == groupId);
                    return exists;
                });
        }
    }
}
