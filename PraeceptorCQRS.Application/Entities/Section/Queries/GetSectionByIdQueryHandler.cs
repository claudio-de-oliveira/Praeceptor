using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Queries
{
    public class GetSectionByIdQueryHandler
        : IRequestHandler<GetSectionByIdQuery, ErrorOr<SectionResult>>
    {
        private readonly ISectionRepository _repository;

        public GetSectionByIdQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionResult>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            var entity = await _repository.GetSectionById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Section.NotFound;

            return new SectionResult(entity);
        }
    }
}

