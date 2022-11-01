using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.CreateCommand
{
    public class CreateGroupValueCommandHandler
        : IRequestHandler<CreateGroupValueCommand, ErrorOr<GroupValueResult>>
    {
        private readonly IGroupValueRepository _repository;

        public CreateGroupValueCommandHandler(IGroupValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupValueResult>> Handle(CreateGroupValueCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            var entity = new Domain.Entities.GroupValue()
            {
                Value = request.Value,
                GroupId = request.GroupId
            };

            var created = await _repository.CreateGroupValue(entity);
            if (created is null)
                return Domain.Errors.Error.GroupValue.DataBaseError;

            return new GroupValueResult(created);
        }
    }
}
