using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public class GetPreceptorByIdQueryHandler
        : IRequestHandler<GetPreceptorByIdQuery, ErrorOr<PreceptorResult>>
    {
        private readonly IPreceptorRepository _repository;

        public GetPreceptorByIdQueryHandler(IPreceptorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorResult>> Handle(GetPreceptorByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            var entity = await _repository.GetPreceptorById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Preceptor.NotFound;

            return new PreceptorResult(entity);
        }
    }
}

