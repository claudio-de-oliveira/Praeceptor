using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public class DeleteChapterCommandHandler
        : IRequestHandler<DeleteChapterCommand, ErrorOr<ChapterResult>>
    {
        private readonly IChapterRepository _repository;

        public DeleteChapterCommandHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterResult>> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var entity = await _repository.GetChapterById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Chapter.NotFound;

            await _repository.DeleteChapter(request.Id);

            return new ChapterResult(entity);
        }
    }
}

