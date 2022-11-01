namespace PraeceptorCQRS.Application.Persistence
{
    public interface IInstituteRepository
    {
        Task<int> GetInstitutesCountByHolding(Guid holdingId);
        Task<Domain.Entities.PageOf<Domain.Entities.Institute>> GetInstitutePage(
            Guid holdingId,
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
        Task<List<Domain.Entities.Institute>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.Institute, bool> predicate);
        Task<Domain.Entities.Institute?> GetInstituteById(Guid id);
        Task<Domain.Entities.Institute?> GetInstituteByCode(string code);
        Task<Domain.Entities.Institute?> CreateInstitute(Domain.Entities.Institute entityToCreate);
        Task UpdateInstitute(Domain.Entities.Institute entityToUpdate);
        Task DeleteInstitute(Guid id);
    }
}

