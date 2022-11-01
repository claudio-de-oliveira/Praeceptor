using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Chapter.Commands
{
    public class UpdateChapterCommandHandler
        : IRequestHandler<UpdateChapterCommand, ErrorOr<ChapterResult>>
    {
        private readonly IChapterRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateChapterCommandHandler(IChapterRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<ChapterResult>> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetChapterById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Chapter.NotFound;

            var updated = new Domain.Entities.Chapter(request.Id)
            {
                Title = request.Title,
                Text = request.Text,
                // Don't change InstituteId
                InstituteId = entity.InstituteId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = request.UpdatedBy
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            await _repository.UpdateChapter(updated);

            return new ChapterResult(updated);
        }
    }
}

