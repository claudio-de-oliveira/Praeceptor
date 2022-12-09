using PraeceptorCQRS.Contracts.Entities.Course;
using PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;
using PraeceptorCQRS.Contracts.Entities.Preceptor;

namespace PraeceptorCQRS.Contracts.Entities.SocialBody
{
    public record SocialBodyEntryResponse(
        PreceptorResponse Preceptor,
        CourseResponse Course,
        PreceptorRoleTypeResponse Role
        );
}
