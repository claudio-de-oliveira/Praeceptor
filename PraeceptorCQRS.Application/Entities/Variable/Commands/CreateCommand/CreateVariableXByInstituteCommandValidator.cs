using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateVariableXByInstituteCommandValidator : AbstractValidator<CreateVariableXByInstituteCommand>
    {
        public CreateVariableXByInstituteCommandValidator(IVariableXRepository variableRepository, IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.GroupName)
                .NotEmpty();
            RuleFor(x => x.VariableName)
                .NotEmpty();
            // institute must exist
            RuleFor(x => x.GroupId)
                .MustAsync(async (instituteId, cancellation) =>
                {
                    bool exists = await instituteRepository.Exists(o => o.Id == instituteId);
                    return exists;
                });
        }
    }
}
