using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SocialBody.Common;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Queries
{
    public record GetSocialBodyEntriesByCourseQuery(
        Guid CourseId
        ) : IRequest<ErrorOr<SocialBodyEntryListResult>>;
}
