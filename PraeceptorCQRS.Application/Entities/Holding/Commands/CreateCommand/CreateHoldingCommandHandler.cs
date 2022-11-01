using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public class CreateHoldingCommandHandler
        : IRequestHandler<CreateHoldingCommand, ErrorOr<HoldingResult>>
    {
        private readonly IHoldingRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateHoldingCommandHandler(IHoldingRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<HoldingResult>> Handle(CreateHoldingCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            var entity = Domain.Entities.Holding.Create(
                request.Acronym,
                request.Name,
                request.Address,
                _dateTimeProvider.UtcNow,
                string.Empty
            );

            var created = await _repository.CreateHolding(entity);
            if (created is null)
                return Domain.Errors.Error.Holding.DataBaseError;

            return new HoldingResult(created);
        }
    }
}

