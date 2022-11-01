using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Common;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Infrastructure.Persistence
{
    public class DocumentRepository : AbstractRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(PraeceptorCQRSDbContext dbContext)
            : base(dbContext)
        { /* Nothing more todo */ }

        public async Task<int> GetDocumentsCountByInstitute(Guid instituteId)
            => await Count(o => o.InstituteId == instituteId);
        public async Task<List<Document>> GetDocumentByInstitute(Guid instituteId)
            => await ListDefault(o => o.InstituteId == instituteId);
        public async Task<List<Document>> GetDocumentPageByInstitute(Guid instituteId, int start, int count)
            => await PageDefault(o => o.InstituteId == instituteId, start, count);
        public async Task<Document?> CreateDocument(Document entityToCreate)
            => await CreateDefault(entityToCreate);
        public async Task<bool> Exists(Func<Document, bool> predicate)
            => await ReadDefault(predicate) is not null;
        public async Task<Document?> GetDocumentById(Guid id)
            => await ReadDefault(o => o.Id == id);
        public async Task UpdateDocument(Document entityToUpdate)
        {
            DetachLocal(o => o.Id == entityToUpdate.Id);
            await UpdateDefault(entityToUpdate);
        }
        public async Task DeleteDocument(Guid id)
        {
            var entityToDelete = await ReadDefault(o => o.Id == id);

            if (entityToDelete is not null)
                await DeleteDefault(entityToDelete);
        }

        public async Task<PageOf<Document>> GetDocumentPage(
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
            var documents = await GetFilteredDocuments(
                instituteId,
                titleFilter,
                textFilter,
                createdFilter,
                createdByFilter,
                lastModifiedFilter,
                lastModifiedByFilter
            );

            documents = SortBy(documents, sort, ascending);

            int numberOfPages = (documents.Count + (pageSize - 1)) / pageSize;
            int nextPage = page + 1;

            if (nextPage >= numberOfPages)
                nextPage = -1;

            List<Document> entities = documents.Count > (page + 1) * pageSize
                    ? documents.GetRange(page * pageSize, pageSize)
                    : documents.GetRange(page * pageSize, documents.Count - page * pageSize);

            return new PageOf<Document>(
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
                // Documents
                entities
                );
        }

        private async Task<List<Document>> GetFilteredDocuments(
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
            var filteredList = new List<Document>();
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

        private static List<Document> SortBy(List<Document> list, string? column, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(column))
                return list;

            return column switch
            {
                "Title" => Global.SortList(list, x => x.Title, ascending),
                "Text" => Global.SortList(list, x => x.Text, ascending),
                "CreatedBy" => Global.SortList(list, x => x.CreatedBy, ascending),
                "LastModifiedBy" => Global.SortList(list, x => x.LastModifiedBy, ascending),
                _ => list
            };
        }
    }
}

