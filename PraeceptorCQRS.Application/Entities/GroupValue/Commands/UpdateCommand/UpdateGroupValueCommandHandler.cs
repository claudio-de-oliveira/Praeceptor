using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.UpdateCommand
{
    public class UpdateGroupValueCommandHandler
        : IRequestHandler<UpdateGroupValueCommand, ErrorOr<GroupValueResult>>
    {
        private readonly IGroupValueRepository _repository;

        public UpdateGroupValueCommandHandler(IGroupValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupValueResult>> Handle(UpdateGroupValueCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetGroupValueById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.GroupValue.NotFound;

            var updated = new Domain.Entities.GroupValue
            {
                Id = request.Id,
                Value = request.Value,
                GroupId = entity.GroupId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            await _repository.UpdateGroupValue(updated);

            return new GroupValueResult(updated);
        }
    }
}
