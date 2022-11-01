using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public class GetSubSectionByInstituteCountQueryHandler
        : IRequestHandler<GetSubSectionByInstituteCountQuery, ErrorOr<SubSectionCountResult>>
    {
        private readonly ISubSectionRepository _repository;

        public GetSubSectionByInstituteCountQueryHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionCountResult>> Handle(GetSubSectionByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var count = await _repository.GetSubSectionsCountByInstitute(request.InstituteId);

            if (count == -1)
                return Domain.Errors.Error.SubSection.NotFound;

            return new SubSectionCountResult(count);
        }
    }
}
