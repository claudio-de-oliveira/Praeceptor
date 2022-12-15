namespace PraeceptorCQRS.Application.Persistence
{
    public interface IVariableXRepository
    {
        Task<bool> Exist(string groupName, Guid groupId, string variableName, string? curriculum);
        Task<Domain.Entities.VariableX?> GetVariableById(Guid id);
        Task<List<Domain.Entities.VariableX>> GetVariablesByGroupId(Guid id, string? curriculum);
        Task<Domain.Entities.VariableX?> CreateVariable(Domain.Entities.VariableX entityToCreate);
        Task UpdateVariable(Domain.Entities.VariableX entityToUpdate);
        Task<Domain.Entities.VariableX?> DeleteVariable(Guid id);
    }
}
