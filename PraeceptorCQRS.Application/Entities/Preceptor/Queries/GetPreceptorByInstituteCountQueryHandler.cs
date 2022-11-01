using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public class GetPreceptorByInstituteCountQueryHandler
        : IRequestHandler<GetPreceptorByInstituteCountQuery, ErrorOr<PreceptorCountResult>>
    {
        private readonly IPreceptorRepository _repository;

        public GetPreceptorByInstituteCountQueryHandler(IPreceptorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorCountResult>> Handle(GetPreceptorByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            var count = await _repository.GetPreceptorsCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.Preceptor.NotFound;

            return new PreceptorCountResult(count);
        }
    }
}
