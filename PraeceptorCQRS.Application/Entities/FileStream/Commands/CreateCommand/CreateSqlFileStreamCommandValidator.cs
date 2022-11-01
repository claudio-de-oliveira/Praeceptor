using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.FileStream.Commands
{
    public class CreateSqlFileStreamCommandValidator : AbstractValidator<CreateSqlFileStreamCommand>
    {
        public CreateSqlFileStreamCommandValidator(IInstituteRepository repository, IFileStreamRepository fileStreamRepository)
        {
            // institute must exist
            RuleFor(o => o.InstituteId)
                .MustAsync(async (instituteId, cancellation) => await repository.Exists(x => x.Id == instituteId))
                    .WithMessage("O instituto não existe");
            RuleFor(o => o.Name)
                .NotEmpty()
                .MaximumLength(250)
                .MustAsync(async (o, name, cancellation) => await Task.Run(() => !fileStreamRepository.Exists(o.InstituteId, name)))
                    .WithMessage($"Um arquivo com o mesmo nome já existe");
            RuleFor(o => o.Title)
                .MaximumLength(4000);
            RuleFor(o => o.Source)
                .MaximumLength(4000);
            RuleFor(o => o.Description)
                .MaximumLength(4000);
            RuleFor(o => o.ContentType)
                .NotEmpty();
        }
    }
}
