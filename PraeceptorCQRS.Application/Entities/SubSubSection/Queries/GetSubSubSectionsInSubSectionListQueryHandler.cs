using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public class GetSubSubSectionsInSubSectionListQueryHandler
        : IRequestHandler<GetSubSubSectionsInSubSectionListQuery, ErrorOr<SubSubSectionListResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public GetSubSubSectionsInSubSectionListQueryHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionListResult>> Handle(GetSubSubSectionsInSubSectionListQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetSubSubSectionList(
                request.SubSectionId
                );

            if (list is null)
                return Domain.Errors.Error.Section.NotFound;

            return new SubSubSectionListResult(list);
        }
    }
}
