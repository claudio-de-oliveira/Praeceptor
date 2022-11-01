using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public class GetSectionPageQueryHandler
        : IRequestHandler<GetSectionPageQuery, ErrorOr<SectionPageResult>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionPageQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionPageResult>> Handle(GetSectionPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            var list = await _repository.GetSectionPage(
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
                return Domain.Errors.Error.Section.NotFound;

            return new SectionPageResult(list);
        }
    }
}
