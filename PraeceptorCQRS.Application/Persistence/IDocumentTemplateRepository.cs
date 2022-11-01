namespace PraeceptorCQRS.Application.Persistence
{
    public interface IDocumentTemplateRepository
    {
        Task<List<Domain.Entities.DocumentTemplate>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.DocumentTemplate, bool> predicate);
        Task<Domain.Entities.DocumentTemplate?> GetDocumentTemplateById(Guid id);
        Task<Domain.Entities.DocumentTemplate?> CreateDocumentTemplate(Domain.Entities.DocumentTemplate entityToCreate);
        Task UpdateDocumentTemplate(Domain.Entities.DocumentTemplate entityToUpdate);
        Task DeleteDocumentTemplate(Guid id);
    }
}

