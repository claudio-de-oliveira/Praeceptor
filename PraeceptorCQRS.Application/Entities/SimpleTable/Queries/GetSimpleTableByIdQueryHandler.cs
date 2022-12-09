using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries
{
    public class GetSimpleTableByIdQueryHandler
        : IRequestHandler<GetSimpleTableByIdQuery, ErrorOr<SimpleTableResult>>
    {
        private readonly ISimpleTableRepository tableRepository;

        public GetSimpleTableByIdQueryHandler(ISimpleTableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SimpleTableResult>> Handle(GetSimpleTableByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            var entity = await tableRepository.GetTableById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SimpleTable.NotFound;

            return new SimpleTableResult(entity);
        }
    }
}