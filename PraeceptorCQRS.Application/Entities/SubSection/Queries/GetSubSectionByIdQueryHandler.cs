using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Queries
{
    public class GetSubSectionByIdQueryHandler
        : IRequestHandler<GetSubSectionByIdQuery, ErrorOr<SubSectionResult>>
    {
        private readonly ISubSectionRepository _repository;

        public GetSubSectionByIdQueryHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionResult>> Handle(GetSubSectionByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var entity = await _repository.GetSubSectionById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.SubSection.NotFound;

            return new SubSectionResult(entity);
        }
    }
}

