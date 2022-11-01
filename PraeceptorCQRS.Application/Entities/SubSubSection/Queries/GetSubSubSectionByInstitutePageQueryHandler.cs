using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public class GetSubSubSectionByInstitutePageQueryHandler
        : IRequestHandler<GetSubSubSectionByInstitutePageQuery, ErrorOr<SubSubSectionListResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public GetSubSubSectionByInstitutePageQueryHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionListResult>> Handle(GetSubSubSectionByInstitutePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var list = await _repository.GetSubSubSectionPageByInstitute(request.InstituteId, request.PageStart, request.PageSize);

            if (list is null)
                return Domain.Errors.Error.SubSubSection.NotFound;

            return new SubSubSectionListResult(list);
        }
    }
}
