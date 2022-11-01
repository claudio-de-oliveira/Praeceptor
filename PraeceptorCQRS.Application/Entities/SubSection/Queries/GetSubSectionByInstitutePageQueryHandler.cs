using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public class GetSubSectionByInstitutePageQueryHandler
        : IRequestHandler<GetSubSectionByInstitutePageQuery, ErrorOr<SubSectionListResult>>
    {
        private readonly ISubSectionRepository _repository;

        public GetSubSectionByInstitutePageQueryHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionListResult>> Handle(GetSubSectionByInstitutePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var list = await _repository.GetSubSectionPageByInstitute(request.InstituteId, request.PageStart, request.PageSize);

            if (list is null)
                return Domain.Errors.Error.SubSection.NotFound;

            return new SubSectionListResult(list);
        }
    }
}
