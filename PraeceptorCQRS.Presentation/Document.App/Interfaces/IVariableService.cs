using Document.App.Models;

using PraeceptorCQRS.Contracts.Entities.Variable;

namespace Document.App.Interfaces
{
    public interface IVariableService
    {
        Task<bool> Exists(Guid groupId, string code);
        Task<HttpResponseMessage> CreateVariable(CreateVariableRequest request);
        Task<VariableModel?> GetVariableById(Guid id);
        Task<VariableModel?> GetVariableByCode(Guid instituteId, string code);
        Task<int> GetVariableCountByGroup(Guid groupId);
        Task<HttpResponseMessage> GetVariablePage(GetVariablePageRequest request);
        Task<HttpResponseMessage> DeleteVariable(Guid id);
        Task<HttpResponseMessage> DeleteVariablesFromGroup(Guid groupId);
    }
}
