using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Preceptor.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Preceptor.Queries
{
    public class GetPreceptorPageQueryHandler
        : IRequestHandler<GetPreceptorPageQuery, ErrorOr<PreceptorPageResult>>
    {
        private readonly IPreceptorRepository _repository;

        public GetPreceptorPageQueryHandler(IPreceptorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorPageResult>> Handle(GetPreceptorPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            var list = await _repository.GetPreceptorPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CodeFilter,
                request.NameFilter,
                request.EmailFilter,
                request.DegreeTypeFilter,
                request.RegimeTypeFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.Preceptor.NotFound;

            return new PreceptorPageResult(list);
        }
    }
}
