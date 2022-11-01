using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries
{
    public class GetPreceptorDegreeTypeByCodeQueryHandler
        : IRequestHandler<GetPreceptorDegreeTypeByCodeQuery, ErrorOr<PreceptorDegreeTypeResult>>
    {
        private readonly IPreceptorDegreeTypeRepository _repository;

        public GetPreceptorDegreeTypeByCodeQueryHandler(IPreceptorDegreeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorDegreeTypeResult>> Handle(GetPreceptorDegreeTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorDegreeType.Canceled;

            var entity = await _repository.GetPreceptorDegreeTypeByCode(request.InstituteId, request.Code);

            if (entity is null)
                return Domain.Errors.Error.PreceptorDegreeType.NotFound;

            return new PreceptorDegreeTypeResult(entity);
        }
    }
}

