using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.AxisType.Queries
{
    public class GetAxisTypePageQueryHandler
        : IRequestHandler<GetAxisTypePageQuery, ErrorOr<AxisTypePageResult>>
    {
        private readonly IAxisTypeRepository _repository;

        public GetAxisTypePageQueryHandler(IAxisTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<AxisTypePageResult>> Handle(GetAxisTypePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;

            var list = await _repository.GetAxisTypePage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CodeFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.AxisType.NotFound;

            return new AxisTypePageResult(list);
        }
    }
}
