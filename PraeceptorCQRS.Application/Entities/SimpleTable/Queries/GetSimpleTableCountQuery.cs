using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries;

public record GetSimpleTableCountQuery(
    Guid InstituteId
    ) : IRequest<ErrorOr<SimpleTableCountResult>>;