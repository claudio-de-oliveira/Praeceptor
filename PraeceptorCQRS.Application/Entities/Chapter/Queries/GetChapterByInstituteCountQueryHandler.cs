using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Chapter.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Chapter.Queries
{
    public class GetChapterByInstituteCountQueryHandler
        : IRequestHandler<GetChapterByInstituteCountQuery, ErrorOr<ChapterCountResult>>
    {
        private readonly IChapterRepository _repository;

        public GetChapterByInstituteCountQueryHandler(IChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ChapterCountResult>> Handle(GetChapterByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var count = await _repository.GetChaptersCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.Chapter.NotFound;

            return new ChapterCountResult(count);
        }
    }
}
