using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public class GetUserCountQueryHandler
        : IRequestHandler<GetUserCountQuery, ErrorOr<UserCountResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public GetUserCountQueryHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserCountResult>> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            var count = await _repository.GetUsersCount();
            if (count == -1)
                return Domain.Errors.Error.User.NotFound;

            return new UserCountResult(count);
        }
    }
}
