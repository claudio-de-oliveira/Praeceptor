using FluentValidation;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.CreateCommand
{
    public class CreateGroupValueCommandValidator : AbstractValidator<CreateGroupValueCommand>
    {
        public CreateGroupValueCommandValidator(IGroupRepository groupRepository)
        {
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
