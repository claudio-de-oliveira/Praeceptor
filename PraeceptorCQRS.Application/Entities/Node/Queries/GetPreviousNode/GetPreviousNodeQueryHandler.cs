using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Queries
{
    public class GetPreviousNodeQueryHandler
        : IRequestHandler<GetPreviousNodeQuery, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;

        public GetPreviousNodeQueryHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<NodeResult>> Handle(GetPreviousNodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var entity = await _repository.GetAt(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Node.NotFound;
            if (entity.PreviousNodeId is null)
                return Domain.Errors.Error.Node.NotFound;

            var previous = await _repository.GetAt((Guid)entity.PreviousNodeId);
            if (previous is null)
                return Domain.Errors.Error.Node.NotFound;

            return new NodeResult(previous);
        }
    }
}
