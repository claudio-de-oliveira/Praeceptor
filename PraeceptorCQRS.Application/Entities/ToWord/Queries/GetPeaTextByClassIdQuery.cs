using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries;

public record GetPeaTextByClassIdQuery(
    Guid ClassId,
    int Season
    ) : IRequest<ErrorOr<PlannerTextResult>>;