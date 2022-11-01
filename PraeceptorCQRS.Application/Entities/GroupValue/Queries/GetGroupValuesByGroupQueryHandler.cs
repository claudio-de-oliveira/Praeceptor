using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Queries
{
    public class GetGroupValuesByGroupQueryHandler
        : IRequestHandler<GetGroupValuesByGroupQuery, ErrorOr<GroupValueListResult>>
    {
        private readonly IGroupValueRepository _repository;

        public GetGroupValuesByGroupQueryHandler(IGroupValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupValueListResult>> Handle(GetGroupValuesByGroupQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            var list = await _repository.GetGroupValuesByGroup(request.GroupId);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            return new GroupValueListResult(list);
        }
    }
}
