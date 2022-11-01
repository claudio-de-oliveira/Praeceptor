namespace PraeceptorCQRS.Application.Persistence
{
    public interface IPreceptorDegreeTypeRepository
    {
        Task<int> GetPreceptorDegreeTypesCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.PreceptorDegreeType>> GetPreceptorDegreeTypePage(
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
        Task<List<Domain.Entities.PreceptorDegreeType>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.PreceptorDegreeType, bool> predicate);
        Task<Domain.Entities.PreceptorDegreeType?> GetPreceptorDegreeTypeById(Guid id);
        Task<Domain.Entities.PreceptorDegreeType?> GetPreceptorDegreeTypeByCode(Guid instituteId, string code);
        Task<Domain.Entities.PreceptorDegreeType?> CreatePreceptorDegreeType(Domain.Entities.PreceptorDegreeType entityToCreate);
        Task UpdatePreceptorDegreeType(Domain.Entities.PreceptorDegreeType entityToUpdate);
        Task DeletePreceptorDegreeType(Guid id);
    }
}

