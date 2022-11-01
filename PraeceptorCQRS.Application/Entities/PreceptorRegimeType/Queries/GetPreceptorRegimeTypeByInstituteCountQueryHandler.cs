using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries
{
    public class GetPreceptorRegimeTypeByInstituteCountQueryHandler
        : IRequestHandler<GetPreceptorRegimeTypeByInstituteCountQuery, ErrorOr<PreceptorRegimeTypeCountResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;

        public GetPreceptorRegimeTypeByInstituteCountQueryHandler(IPreceptorRegimeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRegimeTypeCountResult>> Handle(GetPreceptorRegimeTypeByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            var count = await _repository.GetPreceptorRegimeTypesCountByInstitute(request.InstituteId);

            if (count == -1)
                return Domain.Errors.Error.PreceptorRegimeType.NotFound;

            return new PreceptorRegimeTypeCountResult(count);
        }
    }
}
