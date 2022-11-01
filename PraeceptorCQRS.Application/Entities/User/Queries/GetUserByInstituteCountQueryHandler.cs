using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public class GetUserByInstituteCountQueryHandler
        : IRequestHandler<GetUserByInstituteCountQuery, ErrorOr<UserCountResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public GetUserByInstituteCountQueryHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserCountResult>> Handle(GetUserByInstituteCountQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            var count = await _repository.GetApplicationUsersCountByInstitute(request.InstituteId);
            if (count == -1)
                return Domain.Errors.Error.Institute.NotFound;

            return new UserCountResult(count);
        }
    }
}
