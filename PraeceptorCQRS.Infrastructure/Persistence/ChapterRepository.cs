using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class ChapterRepository : AbstractRepository<Chapter>, IChapterRepository
    {
        private readonly IListRepository _listRepository;

        public ChapterRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        {
            _listRepository = new ListRepository(dbContext);
        }

        public async Task<int> GetChaptersCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);

        public async Task<List<Chapter>> GetChapterByInstitute(Guid instituteId)
            => await ListDefault(o => o.InstituteId == instituteId);

        public async Task<List<Chapter>> GetChapterPageByInstitute(Guid instituteId, int start, int count)
            => await PageDefault(o => o.InstituteId == instituteId, start, count);

        public async Task<Chapter?> CreateChapter(Chapter entityToCreate)
            => await CreateDefault(entityToCreate);

        public async Task<bool> Exists(Func<Chapter, bool> predicate)
            => await ReadDefault(predicate) is not null;

        public async Task<Chapter?> GetChapterById(Guid id)
            => await ReadDefault(o => o.Id == id);

        public async Task UpdateChapter(Chapter entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }

        public async Task DeleteChapter(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<List<Chapter>> GetChapterList(Guid firstEntityId)
        {
            List<Chapter> list = new();
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

        public async Task<PageOf<Chapter>> GetChapterPage(
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
            var chapters = await GetFilteredEntities(
                instituteId,
                titleFilter,
                textFilter,
                createdFilter,
                createdByFilter,
                lastModifiedFilter,
                lastModifiedByFilter
            );

            chapters = SortBy(chapters, sort, ascending);

            int numberOfPages = (chapters.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Chapter> entities = chapters.Count > (page + 1) * pageSize
                    ? chapters.GetRange(page * pageSize, pageSize)
                    : chapters.GetRange(page * pageSize, chapters.Count - page * pageSize);

            return new PageOf<Chapter>(
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

        private async Task<List<Chapter>> GetFilteredEntities(
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
            var filteredList = new List<Chapter>();
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

        private static List<Chapter> SortBy(List<Chapter> list, string? column, bool ascending)
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