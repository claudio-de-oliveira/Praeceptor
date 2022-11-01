using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public class GetSubSubSectionPageQueryHandler
        : IRequestHandler<GetSubSubSectionPageQuery, ErrorOr<SubSubSectionPageResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public GetSubSubSectionPageQueryHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionPageResult>> Handle(GetSubSubSectionPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var list = await _repository.GetSubSubSectionPage(
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
                return Domain.Errors.Error.SubSubSection.NotFound;

            return new SubSubSectionPageResult(list);
        }
    }
}
