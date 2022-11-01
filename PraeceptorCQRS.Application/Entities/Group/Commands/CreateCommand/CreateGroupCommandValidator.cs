using FluentValidation;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Commands.CreateCommand
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator(IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20);

            // group must exist
            RuleFor(x => x.InstituteId)
                .MustAsync(async (instituteId, cancellation) =>
                {
                    bool exists = await instituteRepository.Exists(o => o.Id == instituteId);
                    return exists;
                });
        }
    }
}
