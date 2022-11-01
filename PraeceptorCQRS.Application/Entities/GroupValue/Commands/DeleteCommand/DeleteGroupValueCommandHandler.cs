using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.GroupValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.DeleteCommand
{
    public class DeleteGroupValueCommandHandler
        : IRequestHandler<DeleteGroupValueCommand, ErrorOr<GroupValueResult>>
    {
        private readonly IGroupValueRepository _repository;

        public DeleteGroupValueCommandHandler(IGroupValueRepository repository, IVariableRepository variableRepository, IVariableValueRepository variableValueRepository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GroupValueResult>> Handle(DeleteGroupValueCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            // Valor do grupo
            var entity = await _repository.GetGroupValueById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.GroupValue.NotFound;

            // Exclusão do valor do grupo
            await _repository.DeleteGroupValue(request.Id);

            return new GroupValueResult(entity);
        }
    }
}
