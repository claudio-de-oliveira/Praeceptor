using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Queries;

public record GetCurriculumsByCourseIdQuery(
    Guid Id
    ) : IRequest<ErrorOr<CurriculumListResult>>;
