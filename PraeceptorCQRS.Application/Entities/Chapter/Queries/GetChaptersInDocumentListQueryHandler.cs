using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public class GetChaptersInDocumentListQueryHandler
        : IRequestHandler<GetChaptersInDocumentListQuery, ErrorOr<ChapterListResult>>
    {
        private readonly IChapterRepository _repository;

        public GetChaptersInDocumentListQueryHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterListResult>> Handle(GetChaptersInDocumentListQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Document.Canceled;

            var list = await _repository.GetChapterList(
                request.DocumentId
                );

            if (list is null)
                return Domain.Errors.Error.Document.NotFound;

            return new ChapterListResult(list);
        }
    }
}
