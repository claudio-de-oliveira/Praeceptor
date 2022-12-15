using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateHoldingVariableXCommandHandler
        : IRequestHandler<CreateHoldingVariableXCommand, ErrorOr<VariableResultX>>
    {
        private readonly IVariableXRepository _repository;

        public CreateHoldingVariableXCommandHandler(IVariableXRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResultX>> Handle(CreateHoldingVariableXCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var groupName = request.GroupName.ToUpper();
            var variableName = request.VariableName.ToUpper();

            if (await _repository.Exist(groupName, request.GroupId, variableName, null))
                return Domain.Errors.Error.VariableX.DuplicateCode;

            if (string.Compare(groupName, "@HOLDING", true) == 0)
            {
                if (string.Compare(variableName, "@ACRONIMO", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@NOME", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@ENDERECO", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
            }

            var created = await _repository.CreateVariable(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = groupName,
                    GroupId = request.GroupId,
                    VariableName = variableName,
                    Value = request.Value,
                    IsDeletable = true
                });
            if (created is null)
                return Domain.Errors.Error.Variable.DataBaseError;

            return new VariableResultX(created);
        }
    }
}
