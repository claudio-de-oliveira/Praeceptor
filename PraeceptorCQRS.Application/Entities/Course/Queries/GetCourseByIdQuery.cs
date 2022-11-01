using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Course.Common;

namespace PraeceptorCQRS.Application.Entities.Course.Queries
{
    public record GetCourseByIdQuery(
        Guid Id
        ) : IRequest<ErrorOr<CourseResult>>;
}

