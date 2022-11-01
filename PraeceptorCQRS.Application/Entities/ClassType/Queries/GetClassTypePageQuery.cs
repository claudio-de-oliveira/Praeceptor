using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public record GetClassTypePageQuery(
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
        ) : IRequest<ErrorOr<ClassTypePageResult>>;
}
