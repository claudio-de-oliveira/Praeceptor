using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface ISubSubSectionRepository
    {
        Task<List<SubSubSection>> QueryDefault(string sql);
        Task<int> GetSubSubSectionsCountByInstitute(Guid instituteId);
        Task<List<SubSubSection>> GetSubSubSectionByInstitute(Guid instituteId);
        Task<List<SubSubSection>> GetSubSubSectionPageByInstitute(Guid instituteId, int start, int count);
        Task<List<SubSubSection>> GetSubSubSectionList(Guid firstEntityId);
        Task<PageOf<SubSubSection>> GetSubSubSectionPage(
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
        Task<bool> Exists(Func<SubSubSection, bool> predicate);
        Task<SubSubSection?> GetSubSubSectionById(Guid id);
        Task<SubSubSection?> CreateSubSubSection(SubSubSection entityToCreate);
        Task UpdateSubSubSection(SubSubSection entityToUpdate);
        Task DeleteSubSubSection(Guid id);
    }
}

