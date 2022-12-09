using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.AxisType.Commands.UpdateCommand
{
    public class UpdateAxisTypeCommandHandler
        : IRequestHandler<UpdateAxisTypeCommand, ErrorOr<AxisTypeResult>>
    {
        private readonly IAxisTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateAxisTypeCommandHandler(IAxisTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<AxisTypeResult>> Handle(UpdateAxisTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAxisTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.AxisType.NotFound;

            var updated = new Domain.Entities.AxisType(request.Id)
            {
                Code = entity.Code,
                Code3 = entity.Code3,
                // don't change institute!
                InstituteId = entity.InstituteId,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;

            await _repository.UpdateAxisType(updated);

            return new AxisTypeResult(updated);
        }
    }
}