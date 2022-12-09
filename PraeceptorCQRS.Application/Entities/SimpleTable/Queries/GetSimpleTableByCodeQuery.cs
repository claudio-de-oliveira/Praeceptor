using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries;

public record GetSimpleTableByCodeQuery(
    string Code,
    Guid InstituteId
    ) : IRequest<ErrorOr<SimpleTableResult>>;