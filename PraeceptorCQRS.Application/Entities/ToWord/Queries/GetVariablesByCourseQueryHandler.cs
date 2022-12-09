using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.ToWord.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ToWord.Queries
{
    public class GetVariablesByCourseQueryHandler
        : IRequestHandler<GetVariablesByCourseQuery, ErrorOr<VariablesResult>>
    {
        private readonly IHoldingRepository _holdingRepository;
        private readonly IInstituteRepository _instituteRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IVariableRepository _variableRepository;
        private readonly IVariableValueRepository _variableValueRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupValueRepository _groupValueRepository;

        public GetVariablesByCourseQueryHandler(
            IHoldingRepository holdingRepository,
            IInstituteRepository instituteRepository,
            ICourseRepository courseRepository,
            IVariableRepository variableRepository,
            IVariableValueRepository variableValueRepository,
            IGroupRepository groupRepository,
            IGroupValueRepository groupValueRepository
            )
        {
            _holdingRepository = holdingRepository;
            _instituteRepository = instituteRepository;
            _courseRepository = courseRepository;
            _variableRepository = variableRepository;
            _variableValueRepository = variableValueRepository;
            _groupRepository = groupRepository;
            _groupValueRepository = groupValueRepository;
        }

        public async Task<ErrorOr<VariablesResult>> Handle(GetVariablesByCourseQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Variable.Canceled;

            Dictionary<string, string> variables = new();
            Dictionary<string, string> info;

            var course = await _courseRepository.GetCourseById(request.CourseId);
            if (course is null)
                return Domain.Errors.Error.Course.NotFound;
            var institute = await _instituteRepository.GetInstituteById(course!.InstituteId);
            if (institute is null)
                return Domain.Errors.Error.Institute.NotFound;
            var holding = await _holdingRepository.GetHoldingById(institute.HoldingId);
            if (holding is null)
                return Domain.Errors.Error.Holding.NotFound;

            info = await GetVariables(institute.Id, "@CURSO", course.Code);
            foreach (var k in info)
                variables.Add(k.Key.ToUpper(), k.Value);
            info = await GetVariables(institute.Id, "@PROFISSAO", course.Code);
            foreach (var k in info)
                variables.Add(k.Key.ToUpper(), k.Value);

            info = await GetVariables(institute.Id, "@IES", institute.Acronym);
            foreach (var k in info)
                variables.Add(k.Key.ToUpper(), k.Value);

            info = await GetVariables(institute.Id, "@EMPRESA", holding.Acronym);
            foreach (var k in info)
                variables.Add(k.Key.ToUpper(), k.Value);

            return new VariablesResult(variables);
        }

        #region Recupera variáveis estáticas tais como @CURSO, @PROFISSAO, @IES e @EMPRESA

        private async Task<Dictionary<string, string>> GetVariables(Guid instituteId, string code, string value)
        {
            Dictionary<string, string> variableValues = new();

            var group = await _groupRepository.GetGroupByCode(instituteId, code);
            if (group is null)
                return variableValues;
            var groupValues = await _groupValueRepository.GetGroupValuesByGroup(group.Id);
            if (groupValues is null)
                return variableValues;
            var groupValue = groupValues.FirstOrDefault(o => o.Value == value);
            if (groupValue is null)
                return variableValues;
            var variables = await _variableRepository.GetVariablesByGroup(group.Id);
            if (variables is null)
                return variableValues;

            foreach (var v in variables)
            {
                var variableValue = await _variableValueRepository.GetVariableValueById(v.Id);
                if (variableValue is not null)
                    variableValues.Add($"{code}.{v.Code}", variableValue.Value);
            }

            return variableValues;
        }

        #endregion Recupera variáveis estáticas tais como @CURSO, @PROFISSAO, @IES e @EMPRESA
    }
}