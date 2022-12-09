using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries
{
    public class GetPreceptorRoleTypeByInstituteCountQueryHandler
        : IRequestHandler<GetPreceptorRoleTypeByInstituteCountQuery, ErrorOr<PreceptorRoleTypeCountResult>>
    {
        private readonly IPreceptorRoleTypeRepository _repository;

        public GetPreceptorRoleTypeByInstituteCountQueryHandler(IPreceptorRoleTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRoleTypeCountResult>> Handle(GetPreceptorRoleTypeByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            var count = await _repository.GetPreceptorRoleTypesCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.PreceptorRoleType.NotFound;

            return new PreceptorRoleTypeCountResult(count);
        }
    }
}
