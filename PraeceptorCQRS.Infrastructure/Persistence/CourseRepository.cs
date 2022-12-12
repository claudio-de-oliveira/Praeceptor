using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class CourseRepository : AbstractRepository<Course>, ICourseRepository
    {
        public CourseRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<Course?> CreateCourse(Course entityToCreate)
            => await CreateDefault(entityToCreate);

        public async Task<bool> Exists(Func<Course, bool> predicate)
            => await ReadDefault(predicate) is not null;

        public async Task<Course?> GetCourseById(Guid id)
            => await ReadDefault(o => o.Id == id);

        public async Task<Course?> GetCourseByCode(string code)
            => await ReadDefault(o => string.Compare(o.Code, code, true) == 0);

        public async Task<int> GetCoursesCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);

        public async Task UpdateCourse(Course entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }

        public async Task DeleteCourse(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Course>> GetCoursePage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? nameFilter,
            int? AC,
            int? NumberOfSeasons,
            int? MinimumWorkload,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var courses = await GetFilteredEntities(
                instituteId,
                codeFilter,
                nameFilter,
                AC,
                NumberOfSeasons,
                MinimumWorkload,
                createdByFilter,
                createdFilter,
                lastModifiedFilter,
                lastModifiedByFilter
                );

            courses = SortBy(courses, sort, ascending);

            int numberOfPages = (courses.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Course> entities = courses.Count > (page + 1) * pageSize
                    ? courses.GetRange(page * pageSize, pageSize)
                    : courses.GetRange(page * pageSize, courses.Count - page * pageSize);

            return new PageOf<Course>(
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

        private async Task<List<Course>> GetFilteredEntities(
            Guid instituteId,
            string? codeFilter,
            string? nameFilter,
            int? AC,
            int? NumberOfSeasons,
            int? MinimumWorkload,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => o.InstituteId == instituteId);
            var filteredList = new List<Course>();
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
                if (AC is not null && AC >= 0)
                {
                    isFiltered = true;

                    if (Global.MatchIntFilter(AC, entity.AC))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (NumberOfSeasons is not null && NumberOfSeasons >= 0)
                {
                    isFiltered = true;

                    if (Global.MatchIntFilter(NumberOfSeasons, entity.NumberOfSeasons))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (MinimumWorkload is not null && MinimumWorkload >= 0)
                {
                    isFiltered = true;

                    if (Global.MatchIntFilter(MinimumWorkload, entity.MinimumWorkload))
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

        private static List<Course> SortBy(List<Course> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Code" => Global.SortList(list, x => x.Code, ascending),
                "Name" => Global.SortList(list, x => x.Name, ascending),
                "AC" => Global.SortList(list, x => x.AC, ascending),
                "Seasons" => Global.SortList(list, x => x.NumberOfSeasons, ascending),
                "MinimumWorkload" => Global.SortList(list, x => x.MinimumWorkload, ascending),
                "Created" => Global.SortList(list, x => x.Created, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModified" => Global.SortList(list, x => x.LastModified, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}