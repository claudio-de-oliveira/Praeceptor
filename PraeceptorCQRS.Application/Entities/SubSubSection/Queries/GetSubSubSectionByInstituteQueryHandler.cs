using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public class GetSubSubSectionByInstituteQueryHandler
        : IRequestHandler<GetSubSubSectionByInstituteQuery, ErrorOr<SubSubSectionListResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public GetSubSubSectionByInstituteQueryHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionListResult>> Handle(GetSubSubSectionByInstituteQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var list = await _repository.GetSubSubSectionByInstitute(request.InstituteId);

            if (list is null)
                return Domain.Errors.Error.SubSubSection.NotFound;

            return new SubSubSectionListResult(list);
        }
    }
}
