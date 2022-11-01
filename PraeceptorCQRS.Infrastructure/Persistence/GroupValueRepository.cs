using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class GroupValueRepository : AbstractRepository<GroupValue>, IGroupValueRepository
    {
        public GroupValueRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<bool> Exists(Func<GroupValue, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<GroupValue?> CreateGroupValue(GroupValue entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<GroupValue?> GetGroupValueById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<List<GroupValue>> GetGroupValuesByGroup(Guid groupId)
            => await ListDefault(o => o.GroupId == groupId);

        public async Task<GroupValue?> UpdateGroupValue(GroupValue entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            return await UpdateDefault(entityToUpdate);
        }
        public async Task<GroupValue?> DeleteGroupValue(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                return await DeleteDefault(entityToDelete);
            return null;
        }

        public async Task<bool> DeleteGroupValuesFromGroup(Guid groupId)
        {
            var values = await base.ListDefault(o => o.GroupId == groupId);

            foreach (var val in values)
            {
                var result = await DeleteGroupValue(val.Id);

                if (result is null)
                    return false;
            }
            return true;
        }
    }
}
