using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand
{
    public class DeleteVariablesFromGroupCommandHandler
        : IRequestHandler<DeleteVariablesFromGroupCommand, ErrorOr<bool>>
    {
        private IVariableRepository _repository;

        public DeleteVariablesFromGroupCommandHandler(IVariableRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteVariablesFromGroupCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            return await _repository.DeleteVariablesFromGroup(request.GroupId);
        }
    }
}
