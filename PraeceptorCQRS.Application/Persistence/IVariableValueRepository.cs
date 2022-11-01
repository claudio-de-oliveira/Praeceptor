namespace PraeceptorCQRS.Application.Persistence
{
    public interface IVariableValueRepository
    {
        Task<bool> Exists(Func<Domain.Entities.VariableValue, bool> predicate);
        Task<Domain.Entities.VariableValue?> CreateVariableValue(Domain.Entities.VariableValue variableValue);
        Task<Domain.Entities.VariableValue?> GetVariableValueById(Guid id);
        Task<List<Domain.Entities.VariableValue>> GetVariableValuesByVariable(Guid variableId);
        Task<Domain.Entities.VariableValue?> UpdateVariableValue(Domain.Entities.VariableValue variableValue);
        Task<Domain.Entities.VariableValue?> GetVariableValueByVariableAndGroupValue(Guid groupValueId, Guid variableId);
        Task<Domain.Entities.VariableValue?> DeleteVariableValue(Guid id);
        Task<bool> DeleteVariableValuesFromVariable(Guid variableId);
    }
}
