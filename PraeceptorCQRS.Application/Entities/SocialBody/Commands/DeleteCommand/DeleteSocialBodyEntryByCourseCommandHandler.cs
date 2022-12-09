using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Commands.DeleteCommand;

public class DeleteSocialBodyEntryByCourseCommandHandler
    : IRequestHandler<DeleteSocialBodyEntryByCourseCommand, ErrorOr<bool>>
{
    private readonly ISocialBodyRepository _repository;

    public DeleteSocialBodyEntryByCourseCommandHandler(ISocialBodyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteSocialBodyEntryByCourseCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.SocialBody.Canceled;

        await _repository.DeleteEntriesByCourse(
            request.CourseId
            );

        return true;
    }
}
