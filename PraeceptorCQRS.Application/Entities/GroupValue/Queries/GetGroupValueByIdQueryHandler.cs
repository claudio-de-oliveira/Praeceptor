using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Queries
{
    public class GetGroupValueByIdQueryHandler
        : IRequestHandler<GetGroupValueByIdQuery, ErrorOr<GroupValueResult>>
    {
        private readonly IGroupValueRepository _repository;

        public GetGroupValueByIdQueryHandler(IGroupValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupValueResult>> Handle(GetGroupValueByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            var entity = await _repository.GetGroupValueById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.GroupValue.NotFound;

            return new GroupValueResult(entity);
        }
    }
}
