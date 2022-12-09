namespace PraeceptorCQRS.Contracts.Entities.SocialBody;

public record CreateSocialBodyEntryRequest(
    Guid CourseId,
    Guid PreceptorId,
    Guid RoleId
    );
