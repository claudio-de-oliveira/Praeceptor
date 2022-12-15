using MediatR;
using ErrorOr;

using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Commands.UpdateCommand
{
    public class UpdateVariableXByInstituteCommandHandler
        : IRequestHandler<UpdateVariableXByInstituteCommand, ErrorOr<VariableResultX>>
    {
        private readonly IVariableXRepository _repository;

        public UpdateVariableXByInstituteCommandHandler(IVariableXRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<VariableResultX>> Handle(UpdateVariableXByInstituteCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var variable = await _repository.GetVariableById(request.Id);
            if (variable is null)
                return Domain.Errors.Error.VariableX.NotFound;

            var updated = new Domain.Entities.VariableX()
            {
                Id = variable.Id,
                GroupName = variable.GroupName,
                GroupId = variable.GroupId,
                Curriculum = variable.Curriculum,
                VariableName = variable.VariableName,
                Value = request.Value,
                IsDeletable = variable.IsDeletable
            };

            await _repository.UpdateVariable(updated);

            return new VariableResultX(updated);
        }
    }
}
