using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SimpleTable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SimpleTable.Queries
{
    public class GetSimpleTablePageQueryHandler
        : IRequestHandler<GetSimpleTablePageQuery, ErrorOr<SimpleTablePageResult>>
    {
        private readonly ISimpleTableRepository tableRepository;

        public GetSimpleTablePageQueryHandler(ISimpleTableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ErrorOr<SimpleTablePageResult>> Handle(GetSimpleTablePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SimpleTable.Canceled;

            var list = await tableRepository.GetTablePage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CodeFilter,
                request.TitleFilter,
                request.HeaderFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.SimpleTable.NotFound;

            return new SimpleTablePageResult(list);
        }
    }
}