using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public class GetHoldingPageQueryHandler
        : IRequestHandler<GetHoldingPageQuery, ErrorOr<HoldingPageResult>>
    {
        private readonly IHoldingRepository _repository;

        public GetHoldingPageQueryHandler(IHoldingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<HoldingPageResult>> Handle(GetHoldingPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            var list = await _repository.GetHoldingPage(
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.AcronymFilter,
                request.NameFilter,
                request.AddressFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.Holding.NotFound;

            return new HoldingPageResult(list);
        }
    }
}
