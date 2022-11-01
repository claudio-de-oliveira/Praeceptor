using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.AxisType.Common;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public record GetAxisTypePageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<AxisTypePageResult>>;
}
