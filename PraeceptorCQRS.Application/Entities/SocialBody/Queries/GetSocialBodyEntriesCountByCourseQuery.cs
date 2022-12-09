using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public record GetSocialBodyEntriesCountByCourseQuery(
        Guid CourseId
        ) : IRequest<ErrorOr<SocialBodyEntriesCount>>;
}
