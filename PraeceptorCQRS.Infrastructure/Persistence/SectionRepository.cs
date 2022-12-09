using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class SectionRepository : AbstractRepository<Section>, ISectionRepository
    {
        private readonly IListRepository _listRepository;

        public SectionRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        {
            _listRepository = new ListRepository(dbContext);
        }

        public async Task<int> GetSectionsCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);

        public async Task<List<Section>> GetSectionByInstitute(Guid instituteId)
            => await ListDefault(o => o.InstituteId == instituteId);

        public async Task<List<Section>> GetSectionPageByInstitute(Guid instituteId, int start, int count)
            => await PageDefault(o => o.InstituteId == instituteId, start, count);

        public async Task<Section?> CreateSection(Section entityToCreate)
            => await CreateDefault(entityToCreate);

        public async Task<bool> Exists(Func<Section, bool> predicate)
            => await ReadDefault(predicate) is not null;

        public async Task<Section?> GetSectionById(Guid id)
            => await ReadDefault(o => o.Id == id);

        public async Task UpdateSection(Section entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }

        public async Task<List<Section>> GetSectionList(Guid firstEntityId)
        {
            List<Section> list = new();
            var position = await _listRepository.GetFirstPosition(firstEntityId);

            while (position is not null)
            {
                var entity = await ReadDefault(o => o.Id == position.SecondEntityId);
                Guard.Against.Null(entity);
                list.Add(entity);
                position = (position.NextNodeId is not null)
                    ? await _listRepository.GetAt(position.NextNodeId)
                    : null;
            }

            return list;
        }

        public async Task DeleteSection(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Section>> GetSectionPage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? titleFilter,
            string? textFilter,
            string? createdFilter,
            string? createdByFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var sections = await GetFilteredEntities(
                instituteId,
                titleFilter,
                textFilter,
                createdFilter,
                createdByFilter,
                lastModifiedFilter,
                lastModifiedByFilter
            );

            sections = SortBy(sections, sort, ascending);

            int numberOfPages = (sections.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Section> entities = sections.Count > (page + 1) * pageSize
                    ? sections.GetRange(page * pageSize, pageSize)
                    : sections.GetRange(page * pageSize, sections.Count - page * pageSize);

            return new PageOf<Section>(
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
                // Sections
                entities
                );
        }

        private async Task<List<Section>> GetFilteredEntities(
            Guid instituteId,
            string? titleFilter,
            string? textFilter,
            string? createdFilter,
            string? createdByFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => o.InstituteId == instituteId);
            var filteredList = new List<Section>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                if (!string.IsNullOrWhiteSpace(titleFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(titleFilter, entity.Title))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(textFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(textFilter, entity.Text))
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

        private static List<Section> SortBy(List<Section> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Title" => Global.SortList(list, x => x.Title, ascending),
                "Text" => Global.SortList(list, x => x.Text, ascending),
                "Created" => Global.SortList(list, x => x.Created, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModified" => Global.SortList(list, x => x.LastModified, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}