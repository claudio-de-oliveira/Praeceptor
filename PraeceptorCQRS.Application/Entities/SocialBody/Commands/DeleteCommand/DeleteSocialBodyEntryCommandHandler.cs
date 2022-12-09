using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Commands.DeleteCommand;

public record DeleteSocialBodyEntryCommandHandler
    : IRequestHandler<DeleteSocialBodyEntryCommand, ErrorOr<bool>>
{
    private readonly ISocialBodyRepository _repository;

    public DeleteSocialBodyEntryCommandHandler(ISocialBodyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteSocialBodyEntryCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.SocialBody.Canceled;

        await _repository.DeleteEntry(
            request.CourseId,
            request.PreceptorId,
            request.RoleId
            );

        return true;
    }
}
