using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface ISocialBodyRepository
    {
        Task<SocialBodyEntry?> CreateEntry(SocialBodyEntry entityToCreate);
        Task<bool> Exists(Func<SocialBodyEntry, bool> predicate);
        Task<SocialBodyEntry?> GetEntry(Guid courseId, Guid preceptorId, Guid roleId);
        Task<List<SocialBodyEntry>> GetEntriesByCourse(Guid courseId);
        Task DeleteEntriesByCourse(Guid courseId);
        Task DeleteEntry(Guid courseId, Guid preceptorId, Guid roleId);
        Task<int> GetEntriesCountByCourse(Guid courseId);
        Task<PageOf<SocialBodyEntry>> GetEntryPage(
            Guid courseId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? CodeFilter,
            string? NameFilter,
            Guid? degreeFilter,
            Guid? regimeFilter,
            Guid? roleFilter
            );
        Task<List<SocialBodyEntry>> QueryDefault(string sql);
    }
}
