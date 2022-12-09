using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Pea.Command
{
    public class CreatePeaCommandValidator : AbstractValidator<CreatePeaCommand>
    {
        public CreatePeaCommandValidator(IClassRepository repository)
        {
            RuleFor(x => x.ClassId)
                .MustAsync(async (classId, cancellation) =>
                {
                    bool exists = await repository.Exists(o => o.Id == classId);
                    return exists;
                });

            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}