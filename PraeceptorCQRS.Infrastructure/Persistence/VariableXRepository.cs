using Org.BouncyCastle.Asn1.Ocsp;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class VariableXRepository : AbstractRepository<VariableX>, IVariableXRepository
    {
        public VariableXRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<bool> Exist(string groupName, Guid groupId, string variableName, int? curriculum)
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

        public async Task<VariableX?> GetVariableById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<List<VariableX>> GetVariablesByGroupId(Guid groupId, int? curriculum)
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

        public async Task<PageOf<VariableX>> GetVariablePage(
            Guid holdingId,
            Guid instituteId,
            Guid courseId,
            int curriculum,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? nameFilter
            )
        {
            var variables = await GetFilteredEntities(
                holdingId,
                instituteId,
                courseId,
                curriculum,
                nameFilter
                );

            variables = SortBy(variables, sort, ascending);

            int numberOfPages = (variables.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<VariableX> entities = variables.Count > (page + 1) * pageSize
                    ? variables.GetRange(page * pageSize, pageSize)
                    : variables.GetRange(page * pageSize, variables.Count - page * pageSize);

            return new PageOf<VariableX>(
                // CurrentPage
                page,
                // Size
                pageSize,
                // PreviousPage
                page - 1,
                // NextPage
                nextPage,
                // NumberOfPages
                numberOfPages,
                // List
                entities
                );
        }

        private async Task<List<VariableX>> GetFilteredEntities(
            Guid holdingId,
            Guid instituteId,
            Guid courseId,
            int curriculum,
            string? nameFilter
            )
        {
            var list = await ListDefault(o => o.GroupId == holdingId || o.GroupId == instituteId 
                || (o.GroupId == courseId && o.Curriculum == curriculum)
                );
            var filteredList = new List<VariableX>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(nameFilter, $"{entity.GroupName}.{entity.VariableName}"))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
            }

            return isFiltered ? filteredList : list;
        }

        private static List<VariableX> SortBy(List<VariableX> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Name" => Global.SortList(list, x => $"{x.GroupName}.{x.VariableName}", ascending),
                _ => list
            };
        }
    }
}

