using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public class GetHoldingByCodeQueryHandler
        : IRequestHandler<GetHoldingByCodeQuery, ErrorOr<HoldingResult>>
    {
        private readonly IHoldingRepository _repository;

        public GetHoldingByCodeQueryHandler(IHoldingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<HoldingResult>> Handle(GetHoldingByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            var entity = await _repository.GetHoldingByCode(request.Code);

            if (entity is null)
                return Domain.Errors.Error.Holding.NotFound;

            return new HoldingResult(entity);
        }
    }
}
