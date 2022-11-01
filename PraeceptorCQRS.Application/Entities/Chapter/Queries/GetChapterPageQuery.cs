using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public record GetChapterPageQuery(
        Guid InstituteId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? TitleFilter,
        string? TextFilter,
        string? CreatedByFilter,
        string? CreatedFilter,
        string? LastModifiedFilter,
        string? LastModifiedByFilter
        ) : IRequest<ErrorOr<ChapterPageResult>>;
}
