using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.VariableValue.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.VariableValue.Commands.CreateCommand
{
    public class CreateVariableValueCommandHandler
        : IRequestHandler<CreateVariableValueCommand, ErrorOr<VariableValueResult>>
    {
        private readonly IVariableValueRepository _repository;

        public CreateVariableValueCommandHandler(IVariableValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableValueResult>> Handle(CreateVariableValueCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.VariableValue()
            {
                GroupValueId = request.GroupValueId,
                Value = request.Value,
                VariableId = request.VariableId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableValue.Canceled;

            var created = await _repository.CreateVariableValue(entity);
            if (created is null)
                return Domain.Errors.Error.VariableValue.DataBaseError;

            return new VariableValueResult(created);
        }
    }
}
