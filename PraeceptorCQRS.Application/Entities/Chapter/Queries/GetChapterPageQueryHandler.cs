using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public class GetChapterPageQueryHandler
        : IRequestHandler<GetChapterPageQuery, ErrorOr<ChapterPageResult>>
    {
        private readonly IChapterRepository _repository;

        public GetChapterPageQueryHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterPageResult>> Handle(GetChapterPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var list = await _repository.GetChapterPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.TitleFilter,
                request.TextFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.Chapter.NotFound;

            return new ChapterPageResult(list);
        }
    }
}
