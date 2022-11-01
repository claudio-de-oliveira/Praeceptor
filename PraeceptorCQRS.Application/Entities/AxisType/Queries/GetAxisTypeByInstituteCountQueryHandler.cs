using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public class GetAxisTypeByInstituteCountQueryHandler
        : IRequestHandler<GetAxisTypeByInstituteCountQuery, ErrorOr<AxisTypeCountResult>>
    {
        private readonly IAxisTypeRepository _repository;

        public GetAxisTypeByInstituteCountQueryHandler(IAxisTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<AxisTypeCountResult>> Handle(GetAxisTypeByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;

            var count = await _repository.GetAxisTypeByInstituteCount(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.AxisType.NotFound;

            return new AxisTypeCountResult(count);
        }
    }
}
