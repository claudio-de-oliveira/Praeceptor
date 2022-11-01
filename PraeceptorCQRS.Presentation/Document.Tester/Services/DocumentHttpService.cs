using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Document;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Document.Tester.Services
{
    internal class DocumentHttpService : HttpService
    {
        public DocumentHttpService(
            string uri,
            string identityServerURI,
            HttpClient httpClient,
            JsonConverter jsonConverter = null!
            )
            : base(uri, identityServerURI, httpClient, jsonConverter)
        {
        }

        protected override async Task<string> GetAccessToken()
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        public async Task<DocumentResponse?> GetDocument(Guid id)
            => await base.GetOne<DocumentResponse>("document", "get", "id", id);
        public async Task<HttpResponseMessage> CreateDocument(CreateDocumentRequest request)
            => await base.Create<CreateDocumentRequest>(request, "document", "create");
        public async Task<HttpResponseMessage> UpdateDocument(UpdateDocumentRequest request)
            => await base.Update(request, "document", "update");
        public async Task<HttpResponseMessage> DeleteDocument(Guid id)
            => await base.Delete("document", "delete", id);

        public async Task<HttpResponseMessage> CreateFirstChapter(CreateFirstNodeRequest request)
            => await base.Create<CreateFirstNodeRequest>(request, "document", "chapter", "create", "first");
        public async Task<HttpResponseMessage> InsertChapterAfterPosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "document", "chapter", "insert", "after");
        public async Task<HttpResponseMessage> InsertChapterBeforePosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "document", "chapter", "insert", "before");
        public async Task<NodeResponse?> GetFirstChapterPosition(Guid id)
            => await base.GetOne<NodeResponse>("document", "chapter", "get", "first", id);
        public async Task<NodeResponse?> GetNextChapterPosition(Guid id)
            => await base.GetOne<NodeResponse>("document", "chapter", "get", "next", id);
        public async Task<NodeResponse?> GetLastChapterPosition(Guid id)
            => await base.GetOne<NodeResponse>("document", "chapter", "get", "last", id);
        public async Task<NodeResponse?> GetPreviousChapterPosition(Guid id)
            => await base.GetOne<NodeResponse>("document", "chapter", "get", "previous", id);
        public async Task<HttpResponseMessage> DeleteChapterAt(Guid id)
            => await base.Delete("document", "chapter", "delete", id);
    }
}
