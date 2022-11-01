using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public class GetAxisTypeByCodeQueryHandler
        : IRequestHandler<GetAxisTypeByCodeQuery, ErrorOr<AxisTypeResult>>
    {
        private readonly IAxisTypeRepository _repository;

        public GetAxisTypeByCodeQueryHandler(IAxisTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<AxisTypeResult>> Handle(GetAxisTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;

            var entity = await _repository.GetAxisTypeByCode(request.InstituteId, request.Code);
            if (entity is null)
                return Domain.Errors.Error.AxisType.NotFound;

            return new AxisTypeResult(entity);
        }
    }
}
