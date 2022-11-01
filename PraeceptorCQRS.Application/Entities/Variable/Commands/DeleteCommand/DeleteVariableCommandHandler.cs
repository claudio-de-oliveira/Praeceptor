using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.DeleteCommand
{
    public class DeleteVariableCommandHandler
        : IRequestHandler<DeleteVariableCommand, ErrorOr<VariableResult>>
    {
        private readonly IVariableRepository _repository;
        private readonly IVariableValueRepository _variableValueRepository;

        public DeleteVariableCommandHandler(IVariableRepository repository, IVariableValueRepository variableValueRepository)
        {
            _repository = repository;
            _variableValueRepository = variableValueRepository;
        }

        public async Task<ErrorOr<VariableResult>> Handle(DeleteVariableCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetVariableById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Variable.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            var result = await _variableValueRepository.DeleteVariableValuesFromVariable(entity.Id);
            if (!result)
                return Domain.Errors.Error.VariableValue.DataBaseError;

            var variable = await _repository.DeleteVariable(request.Id);
            if (variable is null)
                return Domain.Errors.Error.Variable.DataBaseError;

            return new VariableResult(variable);
        }
    }
}
