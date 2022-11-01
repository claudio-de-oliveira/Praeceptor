using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class PreceptorRepository : AbstractRepository<Preceptor>, IPreceptorRepository
    {
        // private readonly AbstractRepository<PreceptorDegreeType> _degreeRepository;
        // private readonly AbstractRepository<PreceptorRegimeType> _regimeRepository;
        private readonly IPreceptorDegreeTypeRepository _degreeRepository;
        private readonly IPreceptorRegimeTypeRepository _regimeRepository;

        public PreceptorRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        {
            _degreeRepository = new PreceptorDegreeTypeRepository(dbContext);
            _regimeRepository = new PreceptorRegimeTypeRepository(dbContext);
        }

        public async Task<Preceptor?> CreatePreceptor(Preceptor entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<Preceptor, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<Preceptor?> GetPreceptorById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task<Preceptor?> GetPreceptorByCode(string code)
            => await ReadDefault(o => string.Compare(o.Code, code, true) == 0);
        public async Task<int> GetPreceptorsCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);
        public async Task UpdatePreceptor(Preceptor entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task DeletePreceptor(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Preceptor>> GetPreceptorPage(
            Guid instituteId,
            int page,
            int pageSize,
            string? sort,
            bool ascending,
            string? codeFilter,
            string? nameFilter,
            string? emailFilter,
            Guid? degreeTypeId,
            Guid? regimeTypeId,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var preceptors = await GetFilteredEntities(
                instituteId,
                codeFilter,
                nameFilter,
                emailFilter,
                degreeTypeId,
                regimeTypeId,
                createdByFilter,
                createdFilter,
                lastModifiedFilter,
                lastModifiedByFilter
                );

            preceptors = SortBy(preceptors, sort, ascending);

            int numberOfPages = (preceptors.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Preceptor> entities = preceptors.Count > (page + 1) * pageSize
                    ? preceptors.GetRange(page * pageSize, pageSize)
                    : preceptors.GetRange(page * pageSize, preceptors.Count - page * pageSize);

            return new PageOf<Preceptor>(
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

        private async Task<List<Preceptor>> GetFilteredEntities(
            Guid instituteId,
            string? codeFilter,
            string? nameFilter,
            string? emailFilter,
            Guid? degreeTypeId,
            Guid? regimeTypeId,
            string? createdByFilter,
            string? createdFilter,
            string? lastModifiedFilter,
            string? lastModifiedByFilter
            )
        {
            var list = await ListDefault(o => o.InstituteId == instituteId);
            var filteredList = new List<Preceptor>();
            bool isFiltered = false;

            foreach (var entity in list)
            {
                var t1 = await _degreeRepository.GetPreceptorDegreeTypeById(entity.DegreeTypeId);
                Guard.Against.Null(t1);
                entity.DegreeType = t1;

                var t2 = await _regimeRepository.GetPreceptorRegimeTypeById(entity.RegimeTypeId);
                Guard.Against.Null(t2);
                entity.RegimeType = t2;

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
                if (!string.IsNullOrWhiteSpace(emailFilter))
                {
                    isFiltered = true;

                    if (Global.MatchStringFilter(emailFilter, entity.Email))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (degreeTypeId is not null && degreeTypeId != Guid.Empty)
                {
                    isFiltered = true;

                    if (Global.MatchGuidFilter(degreeTypeId, entity.DegreeTypeId))
                    {
                        if (filteredList.Find(o => o.Id == entity.Id) is null)
                        {
                            filteredList.Add(entity);
                        }
                        continue;
                    }
                }
                if (regimeTypeId is not null && regimeTypeId != Guid.Empty)
                {
                    isFiltered = true;

                    if (Global.MatchGuidFilter(regimeTypeId, entity.RegimeTypeId))
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

        private static List<Preceptor> SortBy(List<Preceptor> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Code" => Global.SortList(list, x => x.Code, ascending),
                "Name" => Global.SortList(list, x => x.Name, ascending),
                "Email" => Global.SortList(list, x => x.Email, ascending),
                "Regime" => Global.SortList(list, x => x.RegimeType, ascending),
                "Degree" => Global.SortList(list, x => x.DegreeType, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}
