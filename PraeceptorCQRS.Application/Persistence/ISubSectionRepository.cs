using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface ISubSectionRepository
    {
        Task<List<SubSection>> QueryDefault(string sql);
        Task<int> GetSubSectionsCountByInstitute(Guid instituteId);
        Task<List<SubSection>> GetSubSectionByInstitute(Guid instituteId);
        Task<List<SubSection>> GetSubSectionPageByInstitute(Guid instituteId, int start, int count);
        Task<List<SubSection>> GetSubSectionList(Guid firstEntityId);
        Task<PageOf<SubSection>> GetSubSectionPage(
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
        Task<bool> Exists(Func<SubSection, bool> predicate);
        Task<SubSection?> GetSubSectionById(Guid id);
        Task<SubSection?> CreateSubSection(SubSection entityToCreate);
        Task UpdateSubSection(SubSection entityToUpdate);
        Task DeleteSubSection(Guid id);
    }
}

