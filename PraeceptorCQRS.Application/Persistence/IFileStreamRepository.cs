namespace PraeceptorCQRS.Application.Persistence
{
    public interface IFileStreamRepository
    {
        Task<int> GetFileCountByInstitute(Guid instituteId);

        Task<Domain.Entities.SqlFileStream?> GetFileInfo(Guid guid);

        Task<Domain.Entities.SqlFileStream?> StoreFile(Domain.Entities.SqlFileStream itemToStore);

        Task<Domain.Entities.SqlFileStream?> ReadFile(Guid guid);

        Task<Domain.Entities.SqlFileStream?> ReadFile(Guid instituteId, string name);

        bool Exists(Guid instituteId, string name);

        Task<int> DeleteFile(Guid guid);

        Task<Domain.Entities.PageOf<Domain.Entities.SqlFileStream>> GetFilesPage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? nameFilter,
            string? titleFilter,
            string? sourceFilter,
            string? descriptionFilter,
            string? contentTypeFilter,
            string? dateCreatedFilter
            );
    }
}