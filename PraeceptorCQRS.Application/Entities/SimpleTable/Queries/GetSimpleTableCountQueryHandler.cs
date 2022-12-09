using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries
{
    public class GetSimpleTableCountQueryHandler
        : IRequestHandler<GetSimpleTableCountQuery, ErrorOr<SimpleTableCountResult>>
    {
        private readonly ISimpleTableRepository tableRepository;

        public GetSimpleTableCountQueryHandler(ISimpleTableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SimpleTableCountResult>> Handle(GetSimpleTableCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            var count = await tableRepository.GetTablesCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.SimpleTable.NotFound;

            return new SimpleTableCountResult(count);
        }
    }
}