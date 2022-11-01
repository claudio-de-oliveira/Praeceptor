using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Pea.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Pea.Queries
{
    public class GetPeasByCodeQueryHandler
        : IRequestHandler<GetPeasByCodeQuery, ErrorOr<PeaPageResult>>
    {
        private readonly IClassRepository _classRepository;
        private readonly IPeaRepository _peaRepository;

        public GetPeasByCodeQueryHandler(IPeaRepository peaRepository, IClassRepository classRepository)
        {
            this._peaRepository = peaRepository;
            this._classRepository = classRepository;
        }

        public async Task<ErrorOr<PeaPageResult>> Handle(GetPeasByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Pea.Canceled;

            var cls = await _classRepository.GetClassByCode(request.ClassCode);
            if (cls is null)
                return Domain.Errors.Error.Class.NotFound;

            var list = await _peaRepository.GetPeaPage(
                cls.Id,
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
