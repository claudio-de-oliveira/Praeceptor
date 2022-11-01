using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public class GetPeaPageQueryHandler
        : IRequestHandler<GetPeaPageQuery, ErrorOr<PeaPageResult>>
    {
        private readonly IPeaRepository _repository;

        public GetPeaPageQueryHandler(IPeaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PeaPageResult>> Handle(GetPeaPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Pea.Canceled;

            var list = await _repository.GetPeaPage(
                request.ClassId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.Pea.NotFound;

            return new PeaPageResult(list);
        }
    }
}
