using Ardalis.GuardClauses;

using Microsoft.EntityFrameworkCore;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class SocialBodyRepository 
        : AbstractRepository<SocialBodyEntry>, ISocialBodyRepository
    {
        public SocialBodyRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        {
            /* Nothing more todo */
        }

        public async Task<SocialBodyEntry?> CreateEntry(SocialBodyEntry entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<SocialBodyEntry, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<SocialBodyEntry?> GetEntry(Guid courseId, Guid preceptorId, Guid roleId)
        {
            var result = await Task.Run(
                () => _context.Set<SocialBodyEntry>()
                    .Include(p => p.Preceptor).Include(p => p.Preceptor.DegreeType).Include(p => p.Preceptor.RegimeType)
                    .Include(p => p.Role)
                    .FirstOrDefault(o => o.CourseId == courseId && o.PreceptorId == preceptorId && o.RoleId == roleId));
            return result;
        }
        // => await ReadDefault(o => o.CourseId == courseId && o.PreceptorId == preceptorId && o.RoleId == roleId);
        public async Task<List<SocialBodyEntry>> GetEntriesByCourse(Guid courseId)
        {
            var result = await Task.Run(
                () => _context.Set<SocialBodyEntry>()
                    .Include(p => p.Preceptor).Include(p => p.Preceptor.DegreeType).Include(p => p.Preceptor.RegimeType)
                    .Include(p => p.Role)
                    .Where(o => o.CourseId == courseId).ToList());
            Guard.Against.Null(result);
            return result;
        }
        // => await ListDefault(o => o.CourseId == courseId);
        public async Task DeleteEntriesByCourse(Guid courseId)
        {
            var entitiesToDelete = await ListDefault(o => o.CourseId == courseId);
            foreach(var entity in entitiesToDelete)
                await DeleteDefault(entity);
        }
        public async Task<int> GetEntriesCountByCourse(Guid courseId)
            => await Count(o => o.CourseId == courseId);

        public async Task DeleteEntry(Guid courseId, Guid preceptorId, Guid roleId)
        {
            var entityToDelete = await ReadDefault(o => o.CourseId == courseId && o.PreceptorId == preceptorId && o.RoleId == roleId);
            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<SocialBodyEntry>> GetEntryPage(
            Guid courseId, 
            int page, 
            int pageSize, 
            string? sort, 
            bool ascending,
            string? codeFilter,
            string? nameFilter,
            Guid? degreeFilter,
            Guid? regimeFilter,
            Guid? roleFilter
            )
        {
            var entries = await GetFilteredEntities(
                courseId,
                codeFilter,
                nameFilter,
                degreeFilter,
                regimeFilter,
                roleFilter
            );
            entries = SortBy(entries, sort, ascending);

            int numberOfPages = (entries.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;
            List<SocialBodyEntry> entities = entries.Count > (page + 1) * pageSize
                ? entries.GetRange(page * pageSize, pageSize)
                : entries.GetRange(page * pageSize, entries.Count - page * pageSize);

            return new PageOf<SocialBodyEntry>(
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

        private async Task<List<SocialBodyEntry>> GetFilteredEntities(
            Guid courseId,
            string? codeFilter,
            string? nameFilter,
            Guid? degreeTypeId,
            Guid? regimeTypeId,
            Guid? roleTypeId
            )
        {
            // ToList() is needed here
            var list = await Task.Run(
                () => _context.Set<SocialBodyEntry>()
                    .Include(p => p.Preceptor).Include(p => p.Preceptor.DegreeType).Include(p => p.Preceptor.RegimeType)
                    .Include(p => p.Role)
                    .Where(o => o.CourseId == courseId).ToList());
            Guard.Against.Null(list);

            var filteredList = new List<SocialBodyEntry>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                if (!string.IsNullOrWhiteSpace(codeFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(codeFilter, entity.Preceptor.Code))
                    {
                        if (filteredList.Find(o => o.Preceptor.Id == entity.Preceptor.Id && o.Role.Id == entity.RoleId) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(nameFilter, entity.Preceptor.Name))
                    {
                        if (filteredList.Find(o => o.Preceptor.Id == entity.Preceptor.Id && o.Role.Id == entity.RoleId) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (degreeTypeId is not null && degreeTypeId != Guid.Empty)
                {
                    isFiltered = true;

                    if (Global.MatchGuidFilter(degreeTypeId, entity.Preceptor.DegreeTypeId))
                    {
                        if (filteredList.Find(o => o.Preceptor.Id == entity.Preceptor.Id && o.Role.Id == entity.RoleId) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (regimeTypeId is not null && regimeTypeId != Guid.Empty)
                {
                    isFiltered = true;

                    if (Global.MatchGuidFilter(regimeTypeId, entity.Preceptor.RegimeTypeId))
                    {
                        if (filteredList.Find(o => o.Preceptor.Id == entity.Preceptor.Id && o.Role.Id == entity.RoleId) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (roleTypeId is not null && roleTypeId != Guid.Empty)
                {
                    isFiltered = true;

                    if (Global.MatchGuidFilter(roleTypeId, entity.Role.Id))
                    {
                        if (filteredList.Find(o => o.Preceptor.Id == entity.Preceptor.Id && o.Role.Id == entity.Role.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
            }

            return isFiltered ? filteredList : list;
        }

        private static List<SocialBodyEntry> SortBy(List<SocialBodyEntry> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Code" => Global.SortList(list, x => x.Preceptor.Code, ascending),
                "Name" => Global.SortList(list, x => x.Preceptor.Name, ascending),
                "Regime" => Global.SortList(list, x => x.Preceptor.RegimeType, ascending),
                "Degree" => Global.SortList(list, x => x.Preceptor.DegreeType, ascending),
                "Role" => Global.SortList(list, x => x.Role.Code, ascending),
                _ => list
            };
        }
    }
}
