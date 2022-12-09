using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries;

public record GetSimpleTableByIdQuery(
    Guid Id
    ) : IRequest<ErrorOr<SimpleTableResult>>;