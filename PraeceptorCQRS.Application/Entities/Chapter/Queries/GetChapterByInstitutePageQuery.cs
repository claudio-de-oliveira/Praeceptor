using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Chapter.Common;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public record GetChapterByInstitutePageQuery(
        Guid InstituteId,
        int PageStart,
        int PageSize
        ) : IRequest<ErrorOr<ChapterListResult>>;
}
