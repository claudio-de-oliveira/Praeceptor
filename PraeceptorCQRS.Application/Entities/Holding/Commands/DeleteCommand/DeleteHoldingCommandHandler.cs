using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Holding.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Holding.Commands
{
    public class DeleteHoldingCommandHandler
        : IRequestHandler<DeleteHoldingCommand, ErrorOr<HoldingResult>>
    {
        private readonly IHoldingRepository _repository;

        public DeleteHoldingCommandHandler(IHoldingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<HoldingResult>> Handle(DeleteHoldingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetHoldingById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Holding.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Holding.Canceled;

            await _repository.DeleteHolding(request.Id);

            return new HoldingResult(entity);
        }
    }
}

