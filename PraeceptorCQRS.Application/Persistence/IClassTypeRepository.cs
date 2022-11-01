namespace PraeceptorCQRS.Application.Persistence
{
    public interface IClassTypeRepository
    {
        Task<int> GetClassTypeCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.ClassType>> GetClassTypePage(
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
        Task<List<Domain.Entities.ClassType>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.ClassType, bool> predicate);
        Task<Domain.Entities.ClassType?> GetClassTypeById(Guid id);
        Task<Domain.Entities.ClassType?> GetClassTypeByCode(Guid instituteId, string code);
        Task<Domain.Entities.ClassType?> CreateClassType(Domain.Entities.ClassType entityToCreate);
        Task UpdateClassType(Domain.Entities.ClassType entityToUpdate);
        Task DeleteClassType(Guid id);
    }
}

