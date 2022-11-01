namespace PraeceptorCQRS.Application.Persistence
{
    public interface IClassRepository
    {
        Task<int> GetClassCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.Class>> GetClassPage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codetitleFilter,
            string? nameFilter,
            int? practice,
            int? theory,
            int? pr,
            Guid? typeId,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            );
        Task<List<Domain.Entities.Class>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.Class, bool> predicate);
        Task<Domain.Entities.Class?> GetClassById(Guid id);
        Task<Domain.Entities.Class?> GetClassByCode(string code);
        Task<Domain.Entities.Class?> CreateClass(Domain.Entities.Class entityToCreate);
        Task UpdateClass(Domain.Entities.Class entityToUpdate);
        Task DeleteClass(Guid id);
    }
}

