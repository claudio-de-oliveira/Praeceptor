using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator(ICourseRepository courseRepository, IInstituteRepository instituteRepository)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(250)
                // code must be unique
                .MustAsync(async (code, cancellation) =>
                {
                    bool exists = await courseRepository.Exists(o => o.Code == code);
                    return !exists;
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            // RuleFor(x => x.Email.Value)
            //     .EmailAddress().When(o => o is not null && !string.IsNullOrWhiteSpace(o.Email.Value));

            // institute must exist
            RuleFor(x => x.InstituteId)
                .MustAsync(async (instituteId, cancellation) =>
                {
                    bool exists = await instituteRepository.Exists(o => o.Id == instituteId);
                    return exists;
                });
        }
    }
}

