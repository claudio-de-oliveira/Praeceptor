using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.CreateCommand
{
    public class CreateVariableXByCourseCommandHandler
        : IRequestHandler<CreateVariableXByCourseCommand, ErrorOr<VariableResultX>>
    {
        private readonly IVariableXRepository _repository;

        public CreateVariableXByCourseCommandHandler(IVariableXRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResultX>> Handle(CreateVariableXByCourseCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var groupName = request.GroupName.ToUpper();
            var variableName = request.VariableName.ToUpper();

            if (await _repository.Exist(groupName, request.GroupId, variableName, request.Curriculum))
                return Domain.Errors.Error.VariableX.DuplicateCode;

            if (string.Compare(groupName, "@CURSO", true) == 0)
            {
                if (string.Compare(variableName, "@CODIGO", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@NOME", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@AC", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@PERIODOS", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@TELEFONE", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
                if (string.Compare(variableName, "@EMAIL", true) == 0)
                    return Domain.Errors.Error.VariableX.DuplicateCode;
            }

            var created = await _repository.CreateVariable(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = groupName,
                    GroupId = request.GroupId,
                    Curriculum = request.Curriculum,
                    VariableName = variableName,
                    Value = request.Value,
                    IsDeletable = true
                });
            if (created is null)
                return Domain.Errors.Error.VariableX.DataBaseError;

            return new VariableResultX(created);
        }
    }
}
