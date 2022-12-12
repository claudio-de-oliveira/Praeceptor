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
                request.Code3,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                string.Empty
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRoleType.Canceled;

            var created = await _repository.CreatePreceptorRoleType(entity);

            if (created is null)
                return Domain.Errors.Error.PreceptorRoleType.DataBaseError;

            return new PreceptorRoleTypeResult(created);
        }
    }
}

