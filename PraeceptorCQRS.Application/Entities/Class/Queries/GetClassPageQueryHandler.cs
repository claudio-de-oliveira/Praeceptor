using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Class.Queries
{
    public class GetClassPageQueryHandler
        : IRequestHandler<GetClassPageQuery, ErrorOr<ClassPageResult>>
    {
        private readonly IClassRepository _repository;

        public GetClassPageQueryHandler(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassPageResult>> Handle(GetClassPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            var list = await _repository.GetClassPage(
                request.InstituteId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.Code,
                request.Name,
                request.Practice,
                request.Theory,
                request.PR,
                request.TypeId,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            if (list is null)
                return Domain.Errors.Error.Class.NotFound;

            return new ClassPageResult(list);
        }
    }
}
