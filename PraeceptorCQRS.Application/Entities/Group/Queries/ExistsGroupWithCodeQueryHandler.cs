using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Queries
{
    public class ExistsGroupWithCodeQueryHandler
        : IRequestHandler<ExistsGroupWithCodeQuery, ErrorOr<GroupExistResult>>
    {
        private readonly IGroupRepository _repository;

        public ExistsGroupWithCodeQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupExistResult>> Handle(ExistsGroupWithCodeQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Group.Canceled;

            var exist = await _repository.Exists(o => o.InstituteId == request.InstituteId && string.Compare(o.Code, request.Code, true) == 0);

            return new GroupExistResult(exist);
        }
    }
}
