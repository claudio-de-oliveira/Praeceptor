using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Commands.CreateCommand;

public record CreateSocialBodyEntryCommand(
    Guid CourseId,
    Guid PreceptorId,
    Guid RoleId
    ) : IRequest<ErrorOr<SocialBodyEntryResult>>;
