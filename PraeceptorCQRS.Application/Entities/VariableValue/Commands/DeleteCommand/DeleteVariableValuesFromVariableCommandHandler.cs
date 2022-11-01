using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.DeleteCommand
{
    public class DeleteVariableValuesFromVariableCommandHandler
        : IRequestHandler<DeleteVariableValuesFromVariableCommand, ErrorOr<bool>>
    {
        private readonly IVariableValueRepository _repository;

        public DeleteVariableValuesFromVariableCommandHandler(IVariableValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteVariableValuesFromVariableCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableValue.Canceled;

            return await _repository.DeleteVariableValuesFromVariable(request.VariableId);
        }
    }
}
