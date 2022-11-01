using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public class GetInstituteByHoldingCountQueryHandler
        : IRequestHandler<GetInstituteByHoldingCountQuery, ErrorOr<InstituteCountResult>>
    {
        private readonly IInstituteRepository _repository;

        public GetInstituteByHoldingCountQueryHandler(IInstituteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<InstituteCountResult>> Handle(GetInstituteByHoldingCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            var count = await _repository.GetInstitutesCountByHolding(request.HoldingId);

            if (count == -1)
                return Domain.Errors.Error.Institute.NotFound;

            return new InstituteCountResult(count);
        }
    }
}
