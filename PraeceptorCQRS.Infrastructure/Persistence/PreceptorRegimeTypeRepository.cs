using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class PreceptorRegimeTypeRepository : AbstractRepository<PreceptorRegimeType>, IPreceptorRegimeTypeRepository
    {
        public PreceptorRegimeTypeRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<PreceptorRegimeType?> CreatePreceptorRegimeType(PreceptorRegimeType entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<PreceptorRegimeType, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<PreceptorRegimeType?> GetPreceptorRegimeTypeById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<PreceptorRegimeType?> GetPreceptorRegimeTypeByCode(Guid instituteId, string code)
            => await ReadDefault(o => o.InstituteId == instituteId && string.Compare(o.Code, code, true) == 0);
        public async Task<int> GetPreceptorRegimeTypesCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);
        public async Task UpdatePreceptorRegimeType(PreceptorRegimeType entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task DeletePreceptorRegimeType(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<PreceptorRegimeType>> GetPreceptorRegimeTypePage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var types = await GetFilteredEntities(
                instituteId,
                codeFilter,
                createdByFilter,
                createdFilter,
                lastModifiedFilter,
                lastModifiedByFilter
                );

            types = SortBy(types, sort, ascending);

            int numberOfPages = (types.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<PreceptorRegimeType> entities = types.Count > (page + 1) * pageSize
                    ? types.GetRange(page * pageSize, pageSize)
                    : types.GetRange(page * pageSize, types.Count - page * pageSize);

            return new PageOf<PreceptorRegimeType>(
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

        private async Task<List<PreceptorRegimeType>> GetFilteredEntities(
            Guid instituteId,
            string? codeFilter,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => o.InstituteId == instituteId);
            var filteredList = new List<PreceptorRegimeType>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
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

        private static List<PreceptorRegimeType> SortBy(List<PreceptorRegimeType> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Code" => Global.SortList(list, x => x.Code, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}

