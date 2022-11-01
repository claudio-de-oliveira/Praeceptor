using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Queries
{
    public class GetPreceptorRegimeTypePageQueryHandler
        : IRequestHandler<GetPreceptorRegimeTypePageQuery, ErrorOr<PreceptorRegimeTypePageResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;

        public GetPreceptorRegimeTypePageQueryHandler(IPreceptorRegimeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRegimeTypePageResult>> Handle(GetPreceptorRegimeTypePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            var list = await _repository.GetPreceptorRegimeTypePage(
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
                return Domain.Errors.Error.PreceptorRegimeType.NotFound;

            return new PreceptorRegimeTypePageResult(list);
        }
    }
}
