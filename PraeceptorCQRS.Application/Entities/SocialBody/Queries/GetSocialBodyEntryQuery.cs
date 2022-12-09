using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public record GetSocialBodyEntryQuery(
        Guid CourseId,
        Guid PreceptorId,
        Guid RoleId
        ) : IRequest<ErrorOr<SocialBodyEntryResult>>;
}
