using ErrorOr;

using MediatR;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Commands.DeleteCommand;

public record DeleteSocialBodyEntryCommand(
    Guid CourseId,
    Guid PreceptorId,
    Guid RoleId
    ) : IRequest<ErrorOr<bool>>;
