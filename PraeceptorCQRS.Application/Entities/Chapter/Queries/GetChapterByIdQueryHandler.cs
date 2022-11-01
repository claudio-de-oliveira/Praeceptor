using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public class GetChapterByIdQueryHandler
        : IRequestHandler<GetChapterByIdQuery, ErrorOr<ChapterResult>>
    {
        private readonly IChapterRepository _repository;

        public GetChapterByIdQueryHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterResult>> Handle(GetChapterByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var entity = await _repository.GetChapterById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Chapter.NotFound;

            return new ChapterResult(entity);
        }
    }
}

