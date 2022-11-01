using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Queries
{
    public record GetComponentByCourseAndCurriculumAndClassQuery(
        Guid CourseId,
        int Curriculum,
        Guid ClassId
        ) : IRequest<ErrorOr<ComponentResult>>;
}
