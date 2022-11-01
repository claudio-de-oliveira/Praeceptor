using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ClassType.Queries
{
    public class GetClassTypePageQueryHandler
        : IRequestHandler<GetClassTypePageQuery, ErrorOr<ClassTypePageResult>>
    {
        private readonly IClassTypeRepository _repository;

        public GetClassTypePageQueryHandler(IClassTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassTypePageResult>> Handle(GetClassTypePageQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetClassTypePage(
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

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            if (list is null)
                return Domain.Errors.Error.ClassType.NotFound;

            return new ClassTypePageResult(list);
        }
    }
}
