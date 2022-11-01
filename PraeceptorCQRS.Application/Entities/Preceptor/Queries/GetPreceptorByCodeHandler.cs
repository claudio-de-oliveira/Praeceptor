using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public class GetPreceptorByCodeHandler
        : IRequestHandler<GetPreceptorByCodeQuery, ErrorOr<PreceptorResult>>
    {
        private readonly IPreceptorRepository _repository;

        public GetPreceptorByCodeHandler(IPreceptorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorResult>> Handle(GetPreceptorByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            var entity = await _repository.GetPreceptorByCode(request.Code);
            if (entity is null)
                return Domain.Errors.Error.Preceptor.NotFound;

            return new PreceptorResult(entity);
        }
    }
}
