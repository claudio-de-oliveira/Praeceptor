using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class ClassRepository : AbstractRepository<Class>, IClassRepository
    {
        private readonly IClassTypeRepository _classTypeRepository;

        public ClassRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        {
            _classTypeRepository = new ClassTypeRepository(dbContext);
        }

        public async Task<Class?> CreateClass(Class entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<Class, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<Class?> GetClassById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<Class?> GetClassByCode(string code)
            => await ReadDefault(o => string.Compare(o.Code, code, true) == 0);
        public async Task UpdateClass(Class entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task<int> GetClassCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);
        public async Task DeleteClass(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Class>> GetClassPage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? nameFilter,
            int? practice,
            int? theory,
            int? pr,
            Guid? typeId,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var classes = await GetFilteredEntities(
                instituteId,
                codeFilter,
                nameFilter,
                practice,
                theory,
                pr,
                typeId,
                createdByFilter,
                createdFilter,
                lastModifiedFilter,
                lastModifiedByFilter
                );

            classes = SortBy(classes, sort, ascending);

            int numberOfPages = (classes.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Class> entities = classes.Count > (page + 1) * pageSize
                    ? classes.GetRange(page * pageSize, pageSize)
                    : classes.GetRange(page * pageSize, classes.Count - page * pageSize);

            return new PageOf<Class>(
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

        private async Task<List<Class>> GetFilteredEntities(
            Guid instituteId,
            string? codeFilter,
            string? nameFilter,
            int? practice,
            int? theory,
            int? pr,
            Guid? typeId,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => o.InstituteId == instituteId);
            var filteredList = new List<Class>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                var t1 = await _classTypeRepository.GetClassTypeById(entity.TypeId);
                Guard.Against.Null(t1);
                entity.Type = t1;

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
                if (practice is not null && practice >= 0)
                {
                    isFiltered = true;

                    if (Global.MatchIntFilter(practice, entity.Practice))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (theory is not null && theory >= 0)
                {
                    isFiltered = true;

                    if (Global.MatchIntFilter(theory, entity.Theory))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (pr is not null && pr >= 0)
                {
                    isFiltered = true;

                    if (Global.MatchIntFilter(pr, entity.PR))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (typeId is not null && typeId != Guid.Empty)
                {
                    isFiltered = true;

                    if (Global.MatchGuidFilter(typeId, entity.TypeId))
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

        private static List<Class> SortBy(List<Class> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Code" => Global.SortList(list, x => x.Code, ascending),
                "Name" => Global.SortList(list, x => x.Name, ascending),
                "Practice" => Global.SortList(list, x => x.Practice, ascending),
                "Theory" => Global.SortList(list, x => x.Theory, ascending),
                "PR" => Global.SortList(list, x => x.PR, ascending),
                "Type" => Global.SortList(list, x => x.Type, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}

