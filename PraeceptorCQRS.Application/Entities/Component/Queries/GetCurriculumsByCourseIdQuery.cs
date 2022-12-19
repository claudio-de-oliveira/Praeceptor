using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Component.Common;

namespace PraeceptorCQRS.Application.Entities.Component.Queries;

public record GetCurriculaByCourseIdQuery(
    Guid Id
    ) : IRequest<ErrorOr<CurriculumListResult>>;
