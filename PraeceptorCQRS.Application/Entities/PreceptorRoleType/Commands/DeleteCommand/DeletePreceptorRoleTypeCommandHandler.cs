using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands
{
    public class DeletePreceptorRoleTypeCommandHandler
        : IRequestHandler<DeletePreceptorRoleTypeCommand, ErrorOr<PreceptorRoleTypeResult>>
    {
        private readonly IPreceptorRoleTypeRepository _repository;

        public DeletePreceptorRoleTypeCommandHandler(IPreceptorRoleTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<PreceptorRoleTypeResult>> Handle(DeletePreceptorRoleTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorRoleTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorRoleType.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            await _repository.DeletePreceptorRoleType(request.Id);

            return new PreceptorRoleTypeResult(entity);
        }
    }
}

