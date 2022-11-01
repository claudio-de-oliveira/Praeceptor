using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public class GetSectionByInstituteQueryHandler
        : IRequestHandler<GetSectionByInstituteQuery, ErrorOr<SectionListResult>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionByInstituteQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionListResult>> Handle(GetSectionByInstituteQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            var list = await _repository.GetSectionByInstitute(request.InstituteId);

            if (list is null)
                return Domain.Errors.Error.Section.NotFound;

            return new SectionListResult(list);
        }
    }
}
