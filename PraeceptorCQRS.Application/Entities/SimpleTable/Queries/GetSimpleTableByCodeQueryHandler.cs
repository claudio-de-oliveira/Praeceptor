using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries
{
    public class GetSimpleTableByCodeQueryHandler
        : IRequestHandler<GetSimpleTableByCodeQuery, ErrorOr<SimpleTableResult>>
    {
        private readonly ISimpleTableRepository tableRepository;

        public GetSimpleTableByCodeQueryHandler(ISimpleTableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SimpleTableResult>> Handle(GetSimpleTableByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            var entity = await tableRepository.GetTableByCode(request.InstituteId, request.Code);
            if (entity is null)
                return Domain.Errors.Error.SimpleTable.NotFound;

            return new SimpleTableResult(entity);
        }
    }
}