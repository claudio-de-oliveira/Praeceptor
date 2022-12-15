using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateCourseVariableXCommandValidator : AbstractValidator<CreateCourseVariableXCommand>
    {
        public CreateCourseVariableXCommandValidator(IVariableXRepository variableRepository, ICourseRepository courseRepository)
        {
            RuleFor(x => x.GroupName)
                .NotEmpty();
            RuleFor(x => x.VariableName)
                .NotEmpty();
            // course must exist
            RuleFor(x => x.GroupId)
                .MustAsync(async (courseId, cancellation) =>
                {
                    bool exists = await courseRepository.Exists(o => o.Id == courseId);
                    return exists;
                });
        }
    }
}
