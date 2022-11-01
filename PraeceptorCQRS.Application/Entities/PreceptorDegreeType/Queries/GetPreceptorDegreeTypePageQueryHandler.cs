using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries
{
    public class GetPreceptorDegreeTypePageQueryHandler
        : IRequestHandler<GetPreceptorDegreeTypePageQuery, ErrorOr<PreceptorDegreeTypePageResult>>
    {
        private readonly IPreceptorDegreeTypeRepository _repository;

        public GetPreceptorDegreeTypePageQueryHandler(IPreceptorDegreeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorDegreeTypePageResult>> Handle(GetPreceptorDegreeTypePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorDegreeType.Canceled;

            var list = await _repository.GetPreceptorDegreeTypePage(
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
                return Domain.Errors.Error.PreceptorDegreeType.NotFound;

            return new PreceptorDegreeTypePageResult(list);
        }
    }
}
