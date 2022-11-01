using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Commands.UpdateCommand
{
    public class MoveBackwardCommandHandler
        : IRequestHandler<MoveBackwardCommand, ErrorOr<bool>>
    {
        private readonly IListRepository _repository;

        public MoveBackwardCommandHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Node?> GetEntityPosition(Guid firstEntityId, Guid secondEntityId)
        {

            var position = await _repository.GetFirstPosition(firstEntityId);

            while (position is not null && position.SecondEntityId != secondEntityId)
            {
                position = await _repository.GetAt(position.NextNodeId);
            }

            return position;
        }

        public async Task<ErrorOr<bool>> Handle(MoveBackwardCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var position = await GetEntityPosition(request.ParentId, request.Id);

            if (position is null)
                return Domain.Errors.Error.Node.NotFound;

            if (position.PreviousNodeId is null)
                return Domain.Errors.Error.Node.BeginList;

            await _repository.MoveBackward(position);

            return true;
        }
    }
}
