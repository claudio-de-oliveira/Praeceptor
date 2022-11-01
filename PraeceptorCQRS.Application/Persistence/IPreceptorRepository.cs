namespace PraeceptorCQRS.Application.Persistence
{
    public interface IPreceptorRepository
    {
        Task<int> GetPreceptorsCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.Preceptor>> GetPreceptorPage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? nameFilter,
            string? emailFilter,
            Guid? degreeTypeId,
            Guid? regimeTypeId,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<List<Domain.Entities.Preceptor>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.Preceptor, bool> predicate);
        Task<Domain.Entities.Preceptor?> GetPreceptorById(Guid id);
        Task<Domain.Entities.Preceptor?> GetPreceptorByCode(string code);
        Task<Domain.Entities.Preceptor?> CreatePreceptor(Domain.Entities.Preceptor entityToCreate);
        Task UpdatePreceptor(Domain.Entities.Preceptor entityToUpdate);
        Task DeletePreceptor(Guid id);
    }
}

