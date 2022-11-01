namespace PraeceptorCQRS.Application.Persistence
{
    public interface IVariableRepository
    {
        Task<bool> Exists(Func<Domain.Entities.Variable, bool> predicate);
        Task<Domain.Entities.Variable?> CreateVariable(Domain.Entities.Variable variable);
        Task<Domain.Entities.Variable?> GetVariableById(Guid id);
        Task<Domain.Entities.Variable?> GetVariableByCode(Guid groupId, string code);
        Task<Domain.Entities.Variable?> DeleteVariable(Guid id);
        Task<bool> DeleteVariablesFromGroup(Guid groupId);
        Task<int> GetVariableCountByGroup(Guid groupId);
        Task<Domain.Entities.PageOf<Domain.Entities.Variable>> GetVariablePage(
            Guid groupId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter
            );
    }
}
