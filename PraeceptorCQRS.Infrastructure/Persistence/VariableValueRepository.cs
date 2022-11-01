using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class VariableValueRepository : AbstractRepository<VariableValue>, IVariableValueRepository
    {
        public VariableValueRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<bool> Exists(Func<VariableValue, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<VariableValue?> CreateVariableValue(VariableValue entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<VariableValue?> GetVariableValueById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<List<VariableValue>> GetVariableValuesByVariable(Guid variableId)
            => await ListDefault(o => o.VariableId == variableId);
        public async Task<VariableValue?> GetVariableValueByVariableAndGroupValue(Guid groupValueId, Guid variableId)
            => await ReadDefault(o => o.GroupValueId == groupValueId && o.VariableId == variableId);

        public async Task<VariableValue?> UpdateVariableValue(VariableValue entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            return await UpdateDefault(entityToUpdate);
        }

        public async Task<bool> DeleteVariableValuesFromVariable(Guid variableId)
        {
            var values = await base.ListDefault(o => o.VariableId == variableId);
        
            foreach (var val in values)
            {
                var result = await DeleteVariableValue(val.Id);
        
                if (result is null)
                    return false;
            }
            return true;
        }

        public async Task<VariableValue?> DeleteVariableValue(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);
        
            if (entityToDelete is not null)
                return await DeleteDefault(entityToDelete);
            return null;
        }
    }
}
