using FluentValidation;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Commands.UpdateCommand
{
    public class UpdateSimpleTableCommandValidator : AbstractValidator<UpdateSimpleTableCommand>
    {
        public UpdateSimpleTableCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}