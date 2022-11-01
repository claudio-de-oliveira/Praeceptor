using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public class GetInstituteByCodeQueryHandler
        : IRequestHandler<GetInstituteByCodeQuery, ErrorOr<InstituteResult>>
    {
        private readonly IInstituteRepository _repository;

        public GetInstituteByCodeQueryHandler(IInstituteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<InstituteResult>> Handle(GetInstituteByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Preceptor.Canceled;

            var entity = await _repository.GetInstituteByCode(request.Code);
            if (entity is null)
                return Domain.Errors.Error.Preceptor.NotFound;

            return new InstituteResult(entity);
        }
    }
}
