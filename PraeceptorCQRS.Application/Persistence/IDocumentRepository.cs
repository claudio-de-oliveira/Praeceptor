using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Persistence
{
    public interface IDocumentRepository
    {
        Task<List<Document>> QueryDefault(string sql);
        Task<int> GetDocumentsCountByInstitute(Guid instituteId);
        Task<List<Document>> GetDocumentByInstitute(Guid instituteId);
        Task<List<Document>> GetDocumentPageByInstitute(Guid instituteId, int start, int count);
        Task<PageOf<Document>> GetDocumentPage(
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
        Task<bool> Exists(Func<Document, bool> predicate);
        Task<Document?> GetDocumentById(Guid id);
        Task<Document?> CreateDocument(Document entityToCreate);
        Task UpdateDocument(Document entityToUpdate);
        Task DeleteDocument(Guid id);
    }
}

