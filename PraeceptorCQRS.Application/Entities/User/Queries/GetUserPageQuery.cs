using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.User.Common;

namespace PraeceptorCQRS.Application.Entities.User.Queries
{
    public record GetUserPageQuery(
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? HoldingIdFilter,
        string? InstituteIdFilter,
        string? CourseIdFilter,
        string? UserNameFilter,
        string? EmailFilter,
        string? PhoneNumberFilter,
        bool? EnabledFilter,
        char? GenderFilter
        ) : IRequest<ErrorOr<UserListResult>>;
}
