namespace PraeceptorCQRS.Application.Persistence
{
    public interface IGroupValueRepository
    {
        Task<bool> Exists(Func<Domain.Entities.GroupValue, bool> predicate);
        Task<Domain.Entities.GroupValue?> CreateGroupValue(Domain.Entities.GroupValue groupValue);
        Task<Domain.Entities.GroupValue?> GetGroupValueById(Guid id);
        Task<List<Domain.Entities.GroupValue>> GetGroupValuesByGroup(Guid groupId);
        Task<Domain.Entities.GroupValue?> UpdateGroupValue(Domain.Entities.GroupValue groupValue);
        Task<Domain.Entities.GroupValue?> DeleteGroupValue(Guid id);
        Task<bool> DeleteGroupValuesFromGroup(Guid groupId);
    }
}
