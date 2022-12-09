using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.PreceptorRoleType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.PreceptorRoleType.Commands
{
    public class CreatePreceptorRoleTypeCommandHandler
        : IRequestHandler<CreatePreceptorRoleTypeCommand, ErrorOr<PreceptorRoleTypeResult>>
    {
        private readonly IPreceptorRoleTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreatePreceptorRoleTypeCommandHandler(IPreceptorRoleTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorRoleTypeResult>> Handle(CreatePreceptorRoleTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.PreceptorRoleType.Create(
                request.Code,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                string.Empty
                );

            // var entity = new Domain.Entities.PreceptorRoleType
            // {
            //     Code = request.Code,
            //     InstituteId = request.InstituteId,
            //     // ...
            //     Created = _dateTimeProvider.UtcNow,
            //     CreatedBy = string.Empty,
            //     LastModified = _dateTimeProvider.UtcNow,
            //     LastModifiedBy = string.Empty
            // };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            var created = await _repository.CreatePreceptorRoleType(entity);

            if (created is null)
                return Domain.Errors.Error.PreceptorRoleType.DataBaseError;

            return new PreceptorRoleTypeResult(created);
        }
    }
}

