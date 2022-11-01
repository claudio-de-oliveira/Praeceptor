using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public class GetInstitutePageQueryHandler
        : IRequestHandler<GetInstitutePageQuery, ErrorOr<InstitutePageResult>>
    {
        private readonly IInstituteRepository _repository;

        public GetInstitutePageQueryHandler(IInstituteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<InstitutePageResult>> Handle(GetInstitutePageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            var list = await _repository.GetInstitutePage(
                request.HoldingId,
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.AcronymFilter,
                request.NameFilter,
                request.AddressFilter,
                request.CreatedByFilter,
                request.CreatedFilter,
                request.LastModifiedFilter,
                request.LastModifiedByFilter
                );

            if (list is null)
                return Domain.Errors.Error.Institute.NotFound;

            return new InstitutePageResult(list);
        }
    }
}
