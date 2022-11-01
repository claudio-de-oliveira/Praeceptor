using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Queries
{
    public class GetNextNodeQueryHandler
        : IRequestHandler<GetNextNodeQuery, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;

        public GetNextNodeQueryHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<NodeResult>> Handle(GetNextNodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var entity = await _repository.GetAt(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Node.NotFound;

            if (entity.NextNodeId is null)
                return Domain.Errors.Error.Node.NotFound;

            var next = await _repository.GetAt((Guid)entity.NextNodeId);
            if (next is null)
                return Domain.Errors.Error.Node.NotFound;

            return new NodeResult(next);
        }
    }
}
