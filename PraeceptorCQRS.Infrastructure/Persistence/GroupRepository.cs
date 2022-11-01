using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class GroupRepository : AbstractRepository<Group>, IGroupRepository
    {
        public GroupRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<bool> Exists(Func<Group, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<Group?> CreateGroup(Group entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<Group?> GetGroupById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<Group?> GetGroupByCode(Guid instituteId, string code)
            => await ReadDefault(o => o.InstituteId == instituteId && string.Compare(o.Code, code, true) == 0);
        public async Task<Group?> DeleteGroup(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                return await DeleteDefault(entityToDelete);
            return null;
        }

        public async Task<int> GetGroupCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);

        public async Task<PageOf<Group>> GetGroupPage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? codeFilter
            )
        {
            var groups = await GetFilteredEntities(instituteId, codeFilter);

            groups = SortBy(groups, sort, ascending);

            int numberOfPages = (groups.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Group> entities = groups.Count > (page + 1) * pageSize
                    ? groups.GetRange(page * pageSize, pageSize)
                    : groups.GetRange(page * pageSize, groups.Count - page * pageSize);

            return new PageOf<Group>(
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

        private async Task<List<Group>> GetFilteredEntities(
            Guid instituteId,
            string? codeFilter
            )
        {
            var list = await ListDefault(o => o.InstituteId == instituteId);
            var filteredList = new List<Group>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                var t1 = await base.ReadDefault(o => o.InstituteId == instituteId);
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

        private static List<Group> SortBy(List<Group> list, string? column, bool ascending)
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
