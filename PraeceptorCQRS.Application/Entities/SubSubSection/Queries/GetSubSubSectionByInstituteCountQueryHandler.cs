using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public class GetSubSubSectionByInstituteCountQueryHandler
        : IRequestHandler<GetSubSubSectionByInstituteCountQuery, ErrorOr<SubSubSectionCountResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public GetSubSubSectionByInstituteCountQueryHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionCountResult>> Handle(GetSubSubSectionByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var count = await _repository.GetSubSubSectionsCountByInstitute(request.InstituteId);

            if (count == -1)
                return Domain.Errors.Error.SubSubSection.NotFound;

            return new SubSubSectionCountResult(count);
        }
    }
}
