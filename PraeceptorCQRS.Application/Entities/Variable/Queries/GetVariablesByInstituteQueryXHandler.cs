using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Variable.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Variable.Queries
{
    public class GetVariablesByInstituteQueryXHandler
        : IRequestHandler<GetVariablesByInstituteQueryX, ErrorOr<VariableXListResult>>
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IVariableXRepository _variableRepository;

        public GetVariablesByInstituteQueryXHandler(IVariableXRepository variableRepository, IInstituteRepository instituteRepository)
        {
            _variableRepository = variableRepository;
            _instituteRepository = instituteRepository;
        }

        public async Task<ErrorOr<VariableXListResult>> Handle(GetVariablesByInstituteQueryX request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.VariableX.Canceled;

            var institute = await _instituteRepository.GetInstituteById(request.Id);
            if (institute is null)
                return Domain.Errors.Error.Institute.NotFound;

            var list = await _variableRepository.GetVariablesByGroupId(request.Id, null);
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@IES",
                    GroupId = institute.Id,
                    VariableName = "@ACRONIMO",
                    Value = institute.Acronym,
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@IES",
                    GroupId = institute.Id,
                    VariableName = "@NOME",
                    Value = institute.Name,
                    IsDeletable = false
                }
            );
            list.Add(
                new Domain.Entities.VariableX()
                {
                    Id = Guid.NewGuid(),
                    GroupName = "@IES",
                    GroupId = institute.Id,
                    VariableName = "@ENDERECO",
                    Value = institute.Address,
                    IsDeletable = false
                }
            );

            return new VariableXListResult(list);
        }
    }
}
