namespace PraeceptorCQRS.Application.Persistence
{
    public interface IGroupRepository
    {
        Task<bool> Exists(Func<Domain.Entities.Group, bool> predicate);
        Task<Domain.Entities.Group?> CreateGroup(Domain.Entities.Group group);
        Task<Domain.Entities.Group?> GetGroupById(Guid id);
        Task<Domain.Entities.Group?> GetGroupByCode(Guid instituteId, string code);
        Task<Domain.Entities.Group?> DeleteGroup(Guid id);
        Task<int> GetGroupCountByInstitute(Guid instituteId);
        Task<Domain.Entities.PageOf<Domain.Entities.Group>> GetGroupPage(
            Guid instituteId,
            int start,
            int count,
            string? sort,
            bool ascending,
            string? codeFilter
            );
    }
}
