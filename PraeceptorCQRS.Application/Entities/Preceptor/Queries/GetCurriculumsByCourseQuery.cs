using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries;

public record GetCurriculumsByCourseQuery(
    Guid CourseId
    ) : IRequest<ErrorOr<CurriculumResultList>>;
