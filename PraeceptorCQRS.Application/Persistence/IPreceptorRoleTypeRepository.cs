namespace PraeceptorCQRS.Application.Persistence
{
    public interface IPreceptorRoleTypeRepository
    {
        Task<int> GetPreceptorRoleTypesCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.PreceptorRoleType>> GetPreceptorRoleTypePage(
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
        Task<List<Domain.Entities.PreceptorRoleType>> QueryDefault(string sql);
        Task<bool> Exists(Func<Domain.Entities.PreceptorRoleType, bool> predicate);
        Task<Domain.Entities.PreceptorRoleType?> GetPreceptorRoleTypeById(Guid id);
        Task<Domain.Entities.PreceptorRoleType?> GetPreceptorRoleTypeByCode(Guid instituteId, string code);
        Task<Domain.Entities.PreceptorRoleType?> CreatePreceptorRoleType(Domain.Entities.PreceptorRoleType entityToCreate);
        Task UpdatePreceptorRoleType(Domain.Entities.PreceptorRoleType entityToUpdate);
        Task DeletePreceptorRoleType(Guid id);
    }
}
