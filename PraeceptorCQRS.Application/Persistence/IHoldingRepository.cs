namespace PraeceptorCQRS.Application.Persistence
{
    public interface IHoldingRepository
    {
        Task<int> GetHoldingsCount();
        Task<Domain.Entities.PageOf<Domain.Entities.Holding>> GetHoldingPage(
            int start,
            int count,
            string? sort,
            bool ascending,
            string? acronymFilter,
            string? nameFilter,
            string? addressFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<List<Domain.Entities.Holding>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.Holding, bool> predicate);
        Task<Domain.Entities.Holding?> GetHoldingById(Guid id);
        Task<Domain.Entities.Holding?> GetHoldingByCode(string code);
        Task<Domain.Entities.Holding?> CreateHolding(Domain.Entities.Holding entityToCreate);
        Task UpdateHolding(Domain.Entities.Holding entityToUpdate);
        Task DeleteHolding(Guid id);
    }
}

