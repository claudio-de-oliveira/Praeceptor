using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Commands
{
    public class DeleteNodeCommandHandler
        : IRequestHandler<DeleteNodeCommand, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;

        public DeleteNodeCommandHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<NodeResult>> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAt(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Node.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            await _repository.Remove(request.Id);

            return new NodeResult(entity);
        }
    }
}
