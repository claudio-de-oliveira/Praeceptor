using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public class GetSubSectionPageQueryHandler
        : IRequestHandler<GetSubSectionPageQuery, ErrorOr<SubSectionPageResult>>
    {
        private readonly ISubSectionRepository _repository;

        public GetSubSectionPageQueryHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionPageResult>> Handle(GetSubSectionPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var list = await _repository.GetSubSectionPage(
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
                return Domain.Errors.Error.SubSection.NotFound;

            return new SubSectionPageResult(list);
        }
    }
}
