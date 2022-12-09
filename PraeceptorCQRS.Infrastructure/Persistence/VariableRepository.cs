using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using Ardalis.GuardClauses;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class VariableRepository : AbstractRepository<Variable>, IVariableRepository
    {
        public VariableRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<bool> Exists(Func<Variable, bool> predicate)
            => await ReadDefault(predicate) is not null;

        public async Task<Variable?> CreateVariable(Variable entityToCreate)
            => await CreateDefault(entityToCreate);

        public async Task<Variable?> GetVariableById(Guid id)
            => await ReadDefault(o => o.Id == id);

        public async Task<Variable?> GetVariableByCode(Guid groupId, string code)
            => await ReadDefault(o => o.GroupId == groupId && string.Compare(o.Code, code, true) == 0);

        public async Task<List<Variable>> GetVariablesByGroup(Guid groupId)
            => await ListDefault(o => o.GroupId == groupId);

        public async Task<Variable?> DeleteVariable(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                return await DeleteDefault(entityToDelete);
            return null;
        }

        public async Task<bool> DeleteVariablesFromGroup(Guid groupId)
        {
            var variables = await base.ListDefault(o => o.GroupId == groupId);

            foreach (var variable in variables)
            {
                var result = await DeleteDefault(variable);
                if (result is null)
                    return false;
            }
            return true;
        }

        public async Task<int> GetVariableCountByGroup(Guid groupId)
            => await Count(o => o.GroupId == groupId);

        public async Task<PageOf<Variable>> GetVariablePage(
            Guid groupId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? codeFilter
            )
        {
            var variables = await GetFilteredEntities(groupId, codeFilter);

            variables = SortBy(variables, sort, ascending);

            int numberOfPages = (variables.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Variable> entities = variables.Count > (page + 1) * pageSize
                    ? variables.GetRange(page * pageSize, pageSize)
                    : variables.GetRange(page * pageSize, variables.Count - page * pageSize);

            return new PageOf<Variable>(
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
                // Chapters
                entities
                );
        }

        private async Task<List<Variable>> GetFilteredEntities(
            Guid groupId,
            string? codeFilter
            )
        {
            var list = await ListDefault(o => o.GroupId == groupId);
            var filteredList = new List<Variable>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                var t1 = await base.ReadDefault(o => o.GroupId == groupId);
                Guard.Against.Null(t1);

                if (!string.IsNullOrWhiteSpace(codeFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(codeFilter, entity.Code))
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

        private static List<Variable> SortBy(List<Variable> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Code" => Global.SortList(list, x => x.Code, ascending),
                _ => list
            };
        }
    }
}