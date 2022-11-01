using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public class GetUserByIdQueryHandler
        : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public GetUserByIdQueryHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            var entity = await _repository.GetApplicationUserById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.User.NotFound;

            return new UserResult(entity);
        }
    }
}
