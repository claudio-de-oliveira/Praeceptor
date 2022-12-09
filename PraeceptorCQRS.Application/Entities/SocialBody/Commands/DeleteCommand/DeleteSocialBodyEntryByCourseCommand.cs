using ErrorOr;
using MediatR;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Commands.DeleteCommand;

public record DeleteSocialBodyEntryByCourseCommand(
    Guid CourseId
    ) : IRequest<ErrorOr<bool>>;
