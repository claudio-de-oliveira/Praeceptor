using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

using System.Diagnostics;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public class GetSectionByInstituteCountQueryHandler
        : IRequestHandler<GetSectionByInstituteCountQuery, ErrorOr<SectionCountResult>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionByInstituteCountQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionCountResult>> Handle(GetSectionByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            var count = await _repository.GetSectionsCountByInstitute(request.InstituteId);

            if (count == -1)
                return Domain.Errors.Error.Section.NotFound;

            return new SectionCountResult(count);
        }
    }
}
