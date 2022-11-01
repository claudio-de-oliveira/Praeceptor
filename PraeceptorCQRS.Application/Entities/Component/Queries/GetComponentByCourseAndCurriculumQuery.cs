using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Queries
{
    public record GetComponentByCourseAndCurriculumQuery(
        Guid CourseId,
        int Curriculum
        ) : IRequest<ErrorOr<ComponentListResult>>;
}
