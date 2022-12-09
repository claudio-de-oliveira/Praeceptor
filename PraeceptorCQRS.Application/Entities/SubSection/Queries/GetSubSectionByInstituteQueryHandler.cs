using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public class GetSubSectionByInstituteQueryHandler
        : IRequestHandler<GetSubSectionByInstituteQuery, ErrorOr<SubSectionListResult>>
    {
        private readonly ISubSectionRepository _repository;

        public GetSubSectionByInstituteQueryHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionListResult>> Handle(GetSubSectionByInstituteQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var list = await _repository.GetSubSectionByInstitute(request.InstituteId);

            if (list is null)
                return Domain.Errors.Error.SubSection.NotFound;

            return new SubSectionListResult(list);
        }
    }
}