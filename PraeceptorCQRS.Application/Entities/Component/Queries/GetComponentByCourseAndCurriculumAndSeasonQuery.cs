using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Queries
{
    public record GetComponentByCourseAndCurriculumAndSeasonQuery(
        Guid CourseId,
        int Curriculum,
        int Season
        ) : IRequest<ErrorOr<ComponentListResult>>;
}
