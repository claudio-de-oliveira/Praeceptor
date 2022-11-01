using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public class CreateChapterCommandHandler
        : IRequestHandler<CreateChapterCommand, ErrorOr<ChapterResult>>
    {
        private readonly IChapterRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateChapterCommandHandler(IChapterRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<ChapterResult>> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.Chapter.Create(
                request.Title,
                request.Text,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                request.CreatedBy);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var created = await _repository.CreateChapter(entity);
            if (created is null)
                return Domain.Errors.Error.Chapter.DataBaseError;

            return new ChapterResult(created);
        }
    }
}
