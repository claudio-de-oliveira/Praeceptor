using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public record GetSocialBodyEntryPageQuery(
        Guid CourseId,
        int Start,
        int Count,
        string? Sort,
        bool Ascending,
        string? CodeFilter,
        string? NameFilter,
        Guid? DegreeFilter,
        Guid? RegimeFilter,
        Guid? RoleFilter
        ) : IRequest<ErrorOr<SocialBodyEntryPageResult>>;
}
