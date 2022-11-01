using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.UpdateCommand
{
    public class UpdateVariableValueCommandHandler
        : IRequestHandler<UpdateVariableValueCommand, ErrorOr<VariableValueResult>>
    {
        private readonly IVariableValueRepository _repository;

        public UpdateVariableValueCommandHandler(IVariableValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableValueResult>> Handle(UpdateVariableValueCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetVariableValueById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.VariableValue.NotFound;

            var updated = new Domain.Entities.VariableValue
            {
                GroupValueId = entity.GroupValueId,
                Id = entity.Id,
                Value = request.Value,
                VariableId = entity.VariableId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableValue.Canceled;

            await _repository.UpdateVariableValue(updated);

            return new VariableValueResult(updated);
        }
    }
}
