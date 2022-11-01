using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Commands.UpdateCommand
{
    public class MoveForwardCommandHandler
        : IRequestHandler<MoveForwardCommand, ErrorOr<bool>>
    {
        private readonly IListRepository _repository;

        public MoveForwardCommandHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Node?> GetEntityPosition(Guid firstEntityId, Guid secondEntityId)
        {
            var position = await _repository.GetFirstPosition(firstEntityId);

            while (position is not null && position.SecondEntityId != secondEntityId)
                position = await _repository.GetAt(position.NextNodeId);

            return position;
        }

        public async Task<ErrorOr<bool>> Handle(MoveForwardCommand request, CancellationToken cancellationToken)
        {
            var position = await GetEntityPosition(request.ParentId, request.Id);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            if (position is null)
                return Domain.Errors.Error.Node.NotFound;

            if (position.NextNodeId is null)
                return Domain.Errors.Error.Node.EndList;

            await _repository.MoveForward(position);

            return true;
        }
    }
}
