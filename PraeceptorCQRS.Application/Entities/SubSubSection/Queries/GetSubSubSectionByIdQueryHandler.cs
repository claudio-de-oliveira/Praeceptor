using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Queries
{
    public class GetSubSubSectionByIdQueryHandler
        : IRequestHandler<GetSubSubSectionByIdQuery, ErrorOr<SubSubSectionResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public GetSubSubSectionByIdQueryHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionResult>> Handle(GetSubSubSectionByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var entity = await _repository.GetSubSubSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SubSubSection.NotFound;

            return new SubSubSectionResult(entity);
        }
    }
}

