using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface IDocxStreamRepository
    {
        Task<int> GetDocxCountByInstitute(Guid instituteId);

        Task<SqlDocxStream?> GetDocxInfo(Guid guid);

        Task<SqlDocxStream?> ReadDocx(Guid guid);

        Task<SqlDocxStream?> StoreDocx(SqlDocxStream stream);

        Task<int> DeleteFile(Guid guid);

        Task<PageOf<SqlDocxStream>> GetDocxPage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? titleFilter,
            string? descriptionFilter,
            string? contentTypeFilter,
            string? dateCreatedFilter,
            string? createdByFilter
            );
    }
}