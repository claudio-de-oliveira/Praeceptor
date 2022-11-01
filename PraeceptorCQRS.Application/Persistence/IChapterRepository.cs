using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface IChapterRepository
    {
        Task<List<Chapter>> QueryDefault(string sql);
        Task<int> GetChaptersCountByInstitute(Guid instituteId);
        Task<List<Chapter>> GetChapterByInstitute(Guid instituteId);
        Task<List<Chapter>> GetChapterPageByInstitute(Guid instituteId, int start, int count);
        Task<List<Chapter>> GetChapterList(Guid firstEntityId);
        Task<PageOf<Chapter>> GetChapterPage(
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
        Task<bool> Exists(Func<Chapter, bool> predicate);
        Task<Chapter?> GetChapterById(Guid id);
        Task<Chapter?> CreateChapter(Chapter entityToCreate);
        Task UpdateChapter(Chapter entityToUpdate);
        Task DeleteChapter(Guid id);
    }
}

