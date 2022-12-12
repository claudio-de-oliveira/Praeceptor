using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands
{
    public class UpdatePreceptorRoleTypeCommandHandler
        : IRequestHandler<UpdatePreceptorRoleTypeCommand, ErrorOr<PreceptorRoleTypeResult>>
    {
        private readonly IPreceptorRoleTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdatePreceptorRoleTypeCommandHandler(IPreceptorRoleTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorRoleTypeResult>> Handle(UpdatePreceptorRoleTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorRoleTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorRoleType.NotFound;

            var updated = new Domain.Entities.PreceptorRoleType(request.Id)
            {
                Code = request.Code,
                Code3 = request.Code3,
                // don't change institute!
                InstituteId = entity.InstituteId,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            await _repository.UpdatePreceptorRoleType(updated);

            return new PreceptorRoleTypeResult(updated);
        }
    }
}

