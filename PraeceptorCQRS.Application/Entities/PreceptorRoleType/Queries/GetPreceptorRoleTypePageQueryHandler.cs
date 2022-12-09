using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries
{
    public class GetPreceptorRoleTypePageQueryHandler
        : IRequestHandler<GetPreceptorRoleTypePageQuery, ErrorOr<PreceptorRoleTypePageResult>>
    {
        private readonly IPreceptorRoleTypeRepository _repository;

        public GetPreceptorRoleTypePageQueryHandler(IPreceptorRoleTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRoleTypePageResult>> Handle(GetPreceptorRoleTypePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            var list = await _repository.GetPreceptorRoleTypePage(
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
                return Domain.Errors.Error.PreceptorRoleType.NotFound;

            return new PreceptorRoleTypePageResult(list);
        }
    }
}
