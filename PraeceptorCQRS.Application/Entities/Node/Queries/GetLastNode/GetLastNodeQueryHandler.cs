using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Queries
{
    public class GetLastNodeQueryHandler
        : IRequestHandler<GetLastNodeQuery, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;

        public GetLastNodeQueryHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<NodeResult>> Handle(GetLastNodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var entity = await _repository.GetLastPosition(request.FirstEntityId);

            if (entity is null)
                return Domain.Errors.Error.Node.NotFound;

            return new NodeResult(entity);
        }
    }
}
