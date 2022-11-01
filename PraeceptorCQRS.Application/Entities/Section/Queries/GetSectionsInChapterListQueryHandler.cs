using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public class GetSectionsInChapterListQueryHandler
        : IRequestHandler<GetSectionsInChapterListQuery, ErrorOr<SectionListResult>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionsInChapterListQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionListResult>> Handle(GetSectionsInChapterListQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Chapter.Canceled;

            var list = await _repository.GetSectionList(
                request.ChapterId
                );

            if (list is null)
                return Domain.Errors.Error.Chapter.NotFound;

            return new SectionListResult(list);
        }
    }
}
