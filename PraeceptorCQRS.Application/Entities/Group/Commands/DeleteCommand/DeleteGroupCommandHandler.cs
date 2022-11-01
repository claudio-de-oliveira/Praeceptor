using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Group.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Group.Commands.DeleteCommand
{
    public class DeleteGroupCommandHandler
        : IRequestHandler<DeleteGroupCommand, ErrorOr<GroupResult>>
    {
        private readonly IGroupRepository _repository;

        public DeleteGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupResult>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetGroupById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Group.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Group.Canceled;

            var group = await _repository.DeleteGroup(request.Id);
            if (group is null)
                return Domain.Errors.Error.Group.DataBaseError;

            return new GroupResult(entity);
        }
    }
}
