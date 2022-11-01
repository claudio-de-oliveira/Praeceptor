using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public class GetSectionByInstitutePageQueryHandler
        : IRequestHandler<GetSectionByInstitutePageQuery, ErrorOr<SectionListResult>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionByInstitutePageQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionListResult>> Handle(GetSectionByInstitutePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            var list = await _repository.GetSectionPageByInstitute(request.InstituteId, request.PageStart, request.PageSize);

            if (list is null)
                return Domain.Errors.Error.Section.NotFound;

            return new SectionListResult(list);
        }
    }
}
