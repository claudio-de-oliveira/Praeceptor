using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class InstituteRepository : AbstractRepository<Institute>, IInstituteRepository
    {
        public InstituteRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<Institute?> CreateInstitute(Institute entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<Institute, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<Institute?> GetInstituteById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<Institute?> GetInstituteByCode(string code)
            => await ReadDefault(o => string.Compare(o.Acronym, code, true) == 0);
        public async Task<int> GetInstitutesCountByHolding(Guid holdingId)
            => await Count(o => o.HoldingId == holdingId);
        public async Task UpdateInstitute(Institute entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task DeleteInstitute(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Institute>> GetInstitutePage(
            Guid holdingId,
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
            string? lastModifiedByFilter
            )
        {
            var institutes = await GetFilteredEntities(
                holdingId,
                acronymFilter,
                nameFilter,
                addressFilter,
                createdByFilter,
                createdFilter,
                lastModifiedFilter,
                lastModifiedByFilter
            );

            institutes = SortBy(institutes, sort, ascending);

            int numberOfPages = (institutes.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Institute> entities = institutes.Count > (page + 1) * pageSize
                    ? institutes.GetRange(page * pageSize, pageSize)
                    : institutes.GetRange(page * pageSize, institutes.Count - page * pageSize);

            return new PageOf<Institute>(
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

        private async Task<List<Institute>> GetFilteredEntities(
            Guid holdingId,
            string? acronymFilter,
            string? nameFilter,
            string? addressFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => o.HoldingId == holdingId);
            var filteredList = new List<Institute>();
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

        private static List<Institute> SortBy(List<Institute> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Acronym" => Global.SortList(list, x => x.Acronym, ascending),
                "Name" => Global.SortList(list, x => x.Name, ascending),
                "Address" => Global.SortList(list, x => x.Address, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}

