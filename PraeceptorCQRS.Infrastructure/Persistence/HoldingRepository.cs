using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class HoldingRepository : AbstractRepository<Holding>, IHoldingRepository
    {
        public HoldingRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<Holding?> CreateHolding(Holding entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<Holding, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<Holding?> GetHoldingById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<Holding?> GetHoldingByCode(string code)
            => await ReadDefault(o => string.Compare(o.Acronym, code, true) == 0);
        public async Task<int> GetHoldingsCount()
            => await Count(o => true);
        public async Task UpdateHolding(Holding entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task DeleteHolding(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Holding>> GetHoldingPage(
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? acronymFilter,
            string? nameFilter,
            string? addressFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter)
        {
            var holdings = await GetFilteredEntities(
                acronymFilter,
                nameFilter,
                addressFilter,
                createdByFilter,
                createdFilter,
                lastModifiedFilter,
                lastModifiedByFilter
                );

            holdings = SortBy(holdings, sort, ascending);

            int numberOfPages = (holdings.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Holding> entities = holdings.Count > (page + 1) * pageSize
                    ? holdings.GetRange(page * pageSize, pageSize)
                    : holdings.GetRange(page * pageSize, holdings.Count - page * pageSize);

            return new PageOf<Holding>(
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

        private async Task<List<Holding>> GetFilteredEntities(
            string? acronymFilter,
            string? nameFilter,
            string? addressFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => true);
            var filteredList = new List<Holding>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                if (!string.IsNullOrWhiteSpace(acronymFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(acronymFilter, entity.Acronym))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(nameFilter, entity.Name))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(addressFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(addressFilter, entity.Address))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(createdFilter))
                {
                    isFiltered = true;

                    if (Global.MatchDateTimeFilter(createdFilter, entity.Created))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(createdByFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(createdByFilter, entity.CreatedBy))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(lastModifiedFilter))
                {
                    isFiltered = true;

                    if (Global.MatchDateTimeFilter(lastModifiedFilter, entity.LastModified))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(lastModifiedByFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(lastModifiedByFilter, entity.LastModifiedBy))
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

        private static List<Holding> SortBy(List<Holding> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Acronym" => Global.SortList(list, x => x.Acronym, ascending),
                "Name" => Global.SortList(list, x => x.Name, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}

