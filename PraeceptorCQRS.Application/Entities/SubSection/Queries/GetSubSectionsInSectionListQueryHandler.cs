using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public class GetSubSectionsInSectionListQueryHandler
        : IRequestHandler<GetSubSectionsInSectionListQuery, ErrorOr<SubSectionListResult>>
    {
        private readonly ISubSectionRepository _repository;

        public GetSubSectionsInSectionListQueryHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionListResult>> Handle(GetSubSectionsInSectionListQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var list = await _repository.GetSubSectionList(
                request.SectionId
                );

            if (list is null)
                return Domain.Errors.Error.Section.NotFound;

            return new SubSectionListResult(list);
        }
    }
}