using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Course.Common;

namespace PraeceptorCQRS.Application.Entities.Course.Queries
{
    public record GetCoursePageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? Code,
        string? Name,
        Guid? CEO,
        int? AC,
        int? NumberOfSeasons,
        int? MinimumWorkload,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<CoursePageResult>>;
}
