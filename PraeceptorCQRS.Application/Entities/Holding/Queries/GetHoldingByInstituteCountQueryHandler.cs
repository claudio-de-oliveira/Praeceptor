using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;

using Serilog;

using System.Diagnostics;

namespace PraeceptorCQRS.Application.Entities.Holding.Queries
{
    public class GetHoldingCountQueryHandler
        : IRequestHandler<GetHoldingByInstituteCountQuery, ErrorOr<HoldingCountResult>>
    {
        private readonly IHoldingRepository _repository;

#if DEBUG
        private readonly Stopwatch stopWatch = new();
#endif

        public GetHoldingCountQueryHandler(IHoldingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<HoldingCountResult>> Handle(GetHoldingByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            var count = await _repository.GetHoldingsCount();
            if (count == -1)
                return Domain.Errors.Error.Holding.NotFound;

            return new HoldingCountResult(count);
        }
    }
}
