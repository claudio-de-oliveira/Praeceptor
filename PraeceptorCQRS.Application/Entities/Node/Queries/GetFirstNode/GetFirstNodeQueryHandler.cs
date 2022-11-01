using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Node.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Node.Queries
{
    public class GetFirstNodeQueryHandler
        : IRequestHandler<GetFirstNodeQuery, ErrorOr<NodeResult>>
    {
        private readonly IListRepository _repository;

        public GetFirstNodeQueryHandler(IListRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<NodeResult>> Handle(GetFirstNodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Node.Canceled;

            var entity = await _repository.GetFirstPosition(request.FirstEntityId);

            if (entity is null)
                return Domain.Errors.Error.Node.NotFound;

            return new NodeResult(entity);
        }
    }
}
