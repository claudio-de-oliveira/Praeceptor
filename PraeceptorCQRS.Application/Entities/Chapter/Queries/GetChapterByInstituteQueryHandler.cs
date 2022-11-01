using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public class GetChapterByInstituteQueryHandler
        : IRequestHandler<GetChapterByInstituteQuery, ErrorOr<ChapterListResult>>
    {
        private readonly IChapterRepository _repository;

        public GetChapterByInstituteQueryHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterListResult>> Handle(GetChapterByInstituteQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var list = await _repository.GetChapterByInstitute(request.InstituteId);
            if (list is null)
                return Domain.Errors.Error.Chapter.NotFound;

            return new ChapterListResult(list);
        }
    }
}
