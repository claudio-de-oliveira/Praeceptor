using FluentValidation;

using PraeceptorCQRS.Application.Entities.Document.Commands;
using PraeceptorCQRS.Application.Entities.SocialBody.Commands.CreateCommand;
using PraeceptorCQRS.Application.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraeceptorCQRS.Application.Entities.CourseSocialBody.Commands.CreateCommand
{
    public class CreateSocialBodyCommandValidator : AbstractValidator<CreateSocialBodyEntryCommand>
    {
        public CreateSocialBodyCommandValidator(
            ICourseRepository courseRepository,
            IPreceptorRepository preceptorRepository,
            IPreceptorRoleTypeRepository preceptorRoleTypeRepository
            )
        {
            // course must exist
            RuleFor(x => x.CourseId)
                .MustAsync(async (courseId, cancellation) =>
                {
                    bool exists = await courseRepository.Exists(o => o.Id == courseId);
                    return exists;
                });
            RuleFor(x => x.PreceptorId)
                .MustAsync(async (preceptorId, cancellation) =>
                {
                    bool exists = await preceptorRepository.Exists(o => o.Id == preceptorId);
                    return exists;
                });
            RuleFor(x => x.RoleId)
                .MustAsync(async (roleId, cancellation) =>
                {
                    bool exists = await preceptorRoleTypeRepository.Exists(o => o.Id == roleId);
                    return exists;
                });
        }
    }
}