using FluentValidation;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.UpdateCommand
{
    public class UpdateVariableValueCommandValidator : AbstractValidator<UpdateVariableValueCommand>
    {
        public UpdateVariableValueCommandValidator()
        {
        }
    }
}
