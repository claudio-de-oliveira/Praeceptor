using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries
{
    public class GetPreceptorRegimeTypeByIdQueryHandler
        : IRequestHandler<GetPreceptorRegimeTypeByIdQuery, ErrorOr<PreceptorRegimeTypeResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;

        public GetPreceptorRegimeTypeByIdQueryHandler(IPreceptorRegimeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRegimeTypeResult>> Handle(GetPreceptorRegimeTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            var entity = await _repository.GetPreceptorRegimeTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorRegimeType.NotFound;

            return new PreceptorRegimeTypeResult(entity);
        }
    }
}

