using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Institute.Queries
{
    public class GetInstituteByIdQueryHandler
        : IRequestHandler<GetInstituteByIdQuery, ErrorOr<InstituteResult>>
    {
        private readonly IInstituteRepository _repository;

        public GetInstituteByIdQueryHandler(IInstituteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<InstituteResult>> Handle(GetInstituteByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            var entity = await _repository.GetInstituteById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Institute.NotFound;

            return new InstituteResult(entity);
        }
    }
}

