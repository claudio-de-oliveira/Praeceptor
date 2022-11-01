using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class UpdateNodeCommandHandler
        : IRequestHandler<UpdateNodeCommand, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateNodeCommandHandler(IListRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<NodeResult>> Handle(UpdateNodeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAt(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Node.NotFound;

            var updated = new Domain.Entities.Node(request.Id)
            {
                PreviousNodeId = entity.PreviousNodeId,
                NextNodeId = entity.NextNodeId,
                FirstEntityId = entity.FirstEntityId,
                SecondEntityId = request.SecondEntityId,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            await _repository.UpdateNode(updated);

            return new NodeResult(updated);
        }
    }
}
