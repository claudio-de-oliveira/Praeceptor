using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public class GetGroupByIdQueryHandler
        : IRequestHandler<GetGroupByIdQuery, ErrorOr<GroupResult>>
    {
        private readonly IGroupRepository _repository;

        public GetGroupByIdQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Group.Canceled;

            var entity = await _repository.GetGroupById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.Group.NotFound;

            return new GroupResult(entity);
        }
    }
}
