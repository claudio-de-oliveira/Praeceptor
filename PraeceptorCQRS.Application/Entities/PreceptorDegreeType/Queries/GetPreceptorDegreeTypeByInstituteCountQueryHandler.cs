using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Queries
{
    public class GetPreceptorDegreeTypeByInstituteCountQueryHandler
        : IRequestHandler<GetPreceptorDegreeTypeByInstituteCountQuery, ErrorOr<PreceptorDegreeTypeCountResult>>
    {
        private readonly IPreceptorDegreeTypeRepository _repository;

        public GetPreceptorDegreeTypeByInstituteCountQueryHandler(IPreceptorDegreeTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorDegreeTypeCountResult>> Handle(GetPreceptorDegreeTypeByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorDegreeType.Canceled;

            var count = await _repository.GetPreceptorDegreeTypesCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.PreceptorDegreeType.NotFound;

            return new PreceptorDegreeTypeCountResult(count);
        }
    }
}
