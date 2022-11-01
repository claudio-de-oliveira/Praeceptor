namespace PraeceptorCQRS.Application.Persistence
{
    public interface IAxisTypeRepository
    {
        Task<int> GetAxisTypeByInstituteCount(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.AxisType>> GetAxisTypePage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<List<Domain.Entities.AxisType>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.AxisType, bool> predicate);
        Task<Domain.Entities.AxisType?> GetAxisTypeById(Guid id);
        Task<Domain.Entities.AxisType?> GetAxisTypeByCode(Guid instituteId, string code);
        Task<Domain.Entities.AxisType?> CreateAxisType(Domain.Entities.AxisType entityToCreate);
        Task UpdateAxisType(Domain.Entities.AxisType entityToUpdate);
        Task DeleteAxisType(Guid id);
    }
}
