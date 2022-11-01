using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public class UpdateHoldingCommandHandler
        : IRequestHandler<UpdateHoldingCommand, ErrorOr<HoldingResult>>
    {
        private readonly IHoldingRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateHoldingCommandHandler(IHoldingRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<HoldingResult>> Handle(UpdateHoldingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetHoldingById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Holding.NotFound;

            var updated = new Domain.Entities.Holding(request.Id)
            {
                Acronym = entity.Acronym,
                Name = request.Name,
                Address = request.Address,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            await _repository.UpdateHolding(updated);

            return new HoldingResult(updated);
        }
    }
}
