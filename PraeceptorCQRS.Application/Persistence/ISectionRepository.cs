using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface ISectionRepository
    {
        Task<List<Section>> QueryDefault(string sql);
        Task<int> GetSectionsCountByInstitute(Guid instituteId);
        Task<List<Section>> GetSectionByInstitute(Guid instituteId);
        Task<List<Section>> GetSectionPageByInstitute(Guid instituteId, int start, int count);
        Task<List<Section>> GetSectionList(Guid firstEntityId);
        Task<PageOf<Section>> GetSectionPage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? titleFilter,
            string? textFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<bool> Exists(Func<Section, bool> predicate);
        Task<Section?> GetSectionById(Guid id);
        Task<Section?> CreateSection(Section entityToCreate);
        Task UpdateSection(Section entityToUpdate);
        Task DeleteSection(Guid id);
    }
}

