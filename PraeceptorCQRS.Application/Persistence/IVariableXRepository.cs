namespace PraeceptorCQRS.Application.Persistence
{
    public interface IVariableXRepository
    {
        Task<bool> Exist(string groupName, Guid groupId, string variableName, int? curriculum);
        Task<Domain.Entities.VariableX?> GetVariableById(Guid id);
        Task<List<Domain.Entities.VariableX>> GetVariablesByGroupId(Guid id, int? curriculum);
        Task<Domain.Entities.VariableX?> CreateVariable(Domain.Entities.VariableX entityToCreate);
        Task UpdateVariable(Domain.Entities.VariableX entityToUpdate);
        Task<Domain.Entities.VariableX?> DeleteVariable(Guid id);
        Task<Domain.Entities.PageOf<Domain.Entities.VariableX>> GetVariablePage(
            Guid holdingId,
            Guid instituteId,
            Guid courseId,
            int curriculum,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? nameFilter
            );
    }
}
