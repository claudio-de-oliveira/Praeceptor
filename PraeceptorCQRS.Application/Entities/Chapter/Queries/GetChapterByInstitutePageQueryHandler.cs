using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public class GetChapterByInstitutePageQueryHandler
        : IRequestHandler<GetChapterByInstitutePageQuery, ErrorOr<ChapterListResult>>
    {
        private readonly IChapterRepository _repository;

        public GetChapterByInstitutePageQueryHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterListResult>> Handle(GetChapterByInstitutePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var list = await _repository.GetChapterPageByInstitute(request.InstituteId, request.PageStart, request.PageSize);
            if (list is null)
                return Domain.Errors.Error.Chapter.NotFound;

            return new ChapterListResult(list);
        }
    }
}
