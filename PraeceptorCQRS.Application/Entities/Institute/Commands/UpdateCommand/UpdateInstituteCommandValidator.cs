using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public class UpdateInstituteCommandValidator : AbstractValidator<UpdateInstituteCommand>
    {
        public UpdateInstituteCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Address)
                .MaximumLength(4000);
        }
    }
}
