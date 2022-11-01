using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Class.Common;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public record GetClassPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code,
        string? Name,
        int Practice,
        int Theory,
        int PR,
        Guid TypeId,
        string? CreatedFilter,
        string? CreatedByFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<ClassPageResult>>;
}
