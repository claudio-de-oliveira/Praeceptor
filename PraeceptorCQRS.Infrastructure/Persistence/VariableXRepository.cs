using Org.BouncyCastle.Asn1.Ocsp;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class VariableXRepository : AbstractRepository<VariableX>, IVariableXRepository
    {
        public VariableXRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<VariableX?> GetVariableById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<List<VariableX>> GetVariablesByGroupId(Guid groupId, string? curriculum)
        {
            if (curriculum is null)
                return await ListDefault(o => o.GroupId == groupId);
            else
                return await ListDefault(o => o.GroupId == groupId && o.Curriculum == curriculum);
        }
        public async Task<VariableX?> CreateVariable(VariableX entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task UpdateVariable(VariableX entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task<VariableX?> DeleteVariable(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is null)
                return null;
            return await DeleteDefault(entityToDelete);
        }

        public async Task<bool> Exist(string groupName, Guid groupId, string variableName, string? curriculum)
        {
            if (curriculum is not null)
            {
                var result = await ReadDefault(o => o.GroupName == groupName && o.GroupId == groupId && o.VariableName == variableName && o.Curriculum == curriculum);
                return result is not null;
            }
            else 
            {
                var result = await ReadDefault(o => o.GroupName == groupName && o.GroupId == groupId && o.VariableName == variableName);
                return result is not null;
            }
        }
    }
}
