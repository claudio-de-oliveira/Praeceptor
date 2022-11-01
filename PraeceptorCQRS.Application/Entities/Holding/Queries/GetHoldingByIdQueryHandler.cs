using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public class GetHoldingByIdQueryHandler
        : IRequestHandler<GetHoldingByIdQuery, ErrorOr<HoldingResult>>
    {
        private readonly IHoldingRepository _repository;

        public GetHoldingByIdQueryHandler(IHoldingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<HoldingResult>> Handle(GetHoldingByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            var entity = await _repository.GetHoldingById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Holding.NotFound;

            return new HoldingResult(entity);
        }
    }
}

