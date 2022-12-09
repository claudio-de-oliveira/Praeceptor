using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.SocialBody.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Entities.SocialBody.Commands.CreateCommand;

public class CreateSocialBodyEntryCommandHandler
    : IRequestHandler<CreateSocialBodyEntryCommand, ErrorOr<SocialBodyEntryResult>>
{
    private readonly ISocialBodyRepository _repository;

    public CreateSocialBodyEntryCommandHandler(ISocialBodyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<SocialBodyEntryResult>> Handle(CreateSocialBodyEntryCommand request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Domain.Errors.Error.SocialBody.Canceled;

        if (await _repository.Exists(o => o.CourseId == request.CourseId && o.PreceptorId == request.PreceptorId && o.RoleId == request.RoleId))
            return Domain.Errors.Error.SocialBody.DuplicateCode;

        var itemToCreate = new SocialBodyEntry
        {
            PreceptorId = request.PreceptorId,
            CourseId = request.CourseId,
            RoleId = request.RoleId
        };

        var created = await _repository.CreateEntry(itemToCreate);
        if (created is null)
            return Domain.Errors.Error.SocialBody.DataBaseError;

        return new SocialBodyEntryResult(created);
    }
}