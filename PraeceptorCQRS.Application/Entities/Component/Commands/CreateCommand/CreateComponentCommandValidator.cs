using FluentValidation;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Component.Commands.CreateCommand
{
    public class CreateComponentCommandValidator : AbstractValidator<CreateComponentCommand>
    {
        public CreateComponentCommandValidator(ICourseRepository courseRepository, IClassRepository classRepository, IAxisTypeRepository axisTypeRepository)
        {
            int seasons = 0;

            RuleFor(o => o.AxisTypeId)
                .MustAsync(async (axisTypeId, cancellation) =>
                {
                    bool exists = await axisTypeRepository.Exists(o => o.Id == axisTypeId);
                    return exists;
                });
            RuleFor(o => o.ClassId)
                .MustAsync(async (classId, cancellation) =>
                {
                    bool exists = await classRepository.Exists(o => o.Id == classId);
                    return exists;
                });
            RuleFor(o => o.CourseId)
                .MustAsync(async (courseId, cancellation) =>
                {
                    var course = await courseRepository.GetCourseById(courseId);
                    if (course is null)
                        return false;
                    seasons = course.NumberOfSeasons;
                    return true;
                });
            RuleFor(o => o.Season)
                .Must(season => season < seasons);
            RuleFor(o => o.Curriculum)
                .NotEmpty();
        }
    }
}
