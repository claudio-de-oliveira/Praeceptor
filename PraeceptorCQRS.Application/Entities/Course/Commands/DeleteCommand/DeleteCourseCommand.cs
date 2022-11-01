using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Course.Common;

namespace PraeceptorCQRS.Application.Entities.Course.Commands
{
    public record DeleteCourseCommand(
        Guid Id
        ) : IRequest<ErrorOr<CourseResult>>;
}

