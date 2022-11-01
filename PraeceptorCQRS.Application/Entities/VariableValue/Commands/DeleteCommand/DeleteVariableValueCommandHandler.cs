using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.DeleteCommand
{
    public class DeleteVariableValueCommandHandler
        : IRequestHandler<DeleteVariableValueCommand, ErrorOr<VariableValueResult>>
    {
        private readonly IVariableValueRepository _repository;

        public DeleteVariableValueCommandHandler(IVariableValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableValueResult>> Handle(DeleteVariableValueCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetVariableValueById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.VariableValue.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableValue.Canceled;

            await _repository.DeleteVariableValue(request.Id);

            return new VariableValueResult(entity);
        }
    }
}
