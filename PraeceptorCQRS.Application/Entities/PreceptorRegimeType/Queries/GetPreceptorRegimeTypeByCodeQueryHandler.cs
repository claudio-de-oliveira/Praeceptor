using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries
{
    public class GetPreceptorRegimeTypeByCodeQueryHandler
        : IRequestHandler<GetPreceptorRegimeTypeByCodeQuery, ErrorOr<PreceptorRegimeTypeResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;

        public GetPreceptorRegimeTypeByCodeQueryHandler(IPreceptorRegimeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRegimeTypeResult>> Handle(GetPreceptorRegimeTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            var entity = await _repository.GetPreceptorRegimeTypeByCode(request.InstituteId, request.Code);

            if (entity is null)
                return Domain.Errors.Error.PreceptorRegimeType.NotFound;

            return new PreceptorRegimeTypeResult(entity);
        }
    }
}

