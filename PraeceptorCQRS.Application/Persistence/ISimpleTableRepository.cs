namespace PraeceptorCQRS.Application.Persistence
{
    public interface ISimpleTableRepository
    {
        Task<int> GetTablesCountByInstitute(Guid instituteId);

        Task<Domain.Entities.PageOf<Domain.Entities.SimpleTable>> GetTablePage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? titleFilter,
            string? columnsFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );

        Task<List<Domain.Entities.SimpleTable>> QueryDefault(string sql);

        Task<bool> Exists(Func<Domain.Entities.SimpleTable, bool> predicate);

        Task<Domain.Entities.SimpleTable?> GetTableById(Guid id);

        Task<Domain.Entities.SimpleTable?> GetTableByCode(Guid instituteId, string code);

        Task<Domain.Entities.SimpleTable?> CreateTable(Domain.Entities.SimpleTable entityToCreate);

        Task UpdateTable(Domain.Entities.SimpleTable entityToUpdate);

        Task DeleteTable(Guid id);
    }
}