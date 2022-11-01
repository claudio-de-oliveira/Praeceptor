using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public class GetUserPageQueryHandler
        : IRequestHandler<GetUserPageQuery, ErrorOr<UserListResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public GetUserPageQueryHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserListResult>> Handle(GetUserPageQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            var list = await _repository.GetApplicationUserPage(
                request.Start,
                request.Count,
                request.Sort,
                request.Ascending,
                request.HoldingIdFilter,
                request.InstituteIdFilter,
                request.CourseIdFilter,
                request.UserNameFilter,
                request.EmailFilter,
                request.PhoneNumberFilter,
                request.EnabledFilter,
                request.GenderFilter
                );

            if (list is null)
                return Domain.Errors.Error.User.NotFound;

            return new UserListResult(list);
        }
    }
}
