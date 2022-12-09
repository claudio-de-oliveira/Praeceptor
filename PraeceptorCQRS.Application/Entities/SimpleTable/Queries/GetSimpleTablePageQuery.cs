using MediatR;
using ErrorOr;
using PraeceptorCQRS.Application.Entities.SimpleTable.Common;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries;

public record GetSimpleTablePageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? TitleFilter,
        string? HeaderFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
    ) : IRequest<ErrorOr<SimpleTablePageResult>>;