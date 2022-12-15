using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class GetVariablesByHoldingQueryXHandler
        : IRequestHandler<GetVariablesByHoldingQueryX, ErrorOr<VariableXListResult>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IVariableXRepository _variableRepository;

        public GetVariablesByHoldingQueryXHandler(IVariableXRepository variableRepository, IHoldingRepository holdingRepository)
        {
            _variableRepository = variableRepository;
            _holdingRepository = holdingRepository;
        }

        public async Task<ErrorOr<VariableXListResult>> Handle(GetVariablesByHoldingQueryX request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var holding = await _holdingRepository.GetHoldingById(request.Id);
            if (holding is null)
                return Domain.Errors.Error.Holding.NotFound;

            var list = await _variableRepository.GetVariablesByGroupId(request.Id, null);
            list.Add(
                new Domain.Entities.VariableX() 
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@HOLDING",
                    GroupId = holding.Id,
                    VariableName = "@ACRONIMO",
                    Value = holding.Acronym,
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@HOLDING",
                    GroupId = holding.Id,
                    VariableName = "@NOME",
                    Value = holding.Name,
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@HOLDING",
                    GroupId = holding.Id,
                    VariableName = "@ENDERECO",
                    Value = holding.Name,
                    IsDeletable = false
                }
            );

            return new VariableXListResult(list);
        }
    }
}
