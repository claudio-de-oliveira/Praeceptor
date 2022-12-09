using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Queries
{
    public class GetPreceptorRoleTypeByIdQueryHandler
        : IRequestHandler<GetPreceptorRoleTypeByIdQuery, ErrorOr<PreceptorRoleTypeResult>>
    {
        private readonly IPreceptorRoleTypeRepository _repository;

        public GetPreceptorRoleTypeByIdQueryHandler(IPreceptorRoleTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRoleTypeResult>> Handle(GetPreceptorRoleTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            var entity = await _repository.GetPreceptorRoleTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorRoleType.NotFound;

            return new PreceptorRoleTypeResult(entity);
        }
    }
}

