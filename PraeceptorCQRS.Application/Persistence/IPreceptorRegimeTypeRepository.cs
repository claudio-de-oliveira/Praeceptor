namespace PraeceptorCQRS.Application.Persistence
{
    public interface IPreceptorRegimeTypeRepository
    {
        Task<int> GetPreceptorRegimeTypesCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.PreceptorRegimeType>> GetPreceptorRegimeTypePage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<List<Domain.Entities.PreceptorRegimeType>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.PreceptorRegimeType, bool> predicate);
        Task<Domain.Entities.PreceptorRegimeType?> GetPreceptorRegimeTypeById(Guid id);
        Task<Domain.Entities.PreceptorRegimeType?> GetPreceptorRegimeTypeByCode(Guid instituteId, string code);
        Task<Domain.Entities.PreceptorRegimeType?> CreatePreceptorRegimeType(Domain.Entities.PreceptorRegimeType entityToCreate);
        Task UpdatePreceptorRegimeType(Domain.Entities.PreceptorRegimeType entityToUpdate);
        Task DeletePreceptorRegimeType(Guid id);
    }
}

