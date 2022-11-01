using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Chapter;
using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Document.Tester.Services
{
    internal class ChapterHttpService : HttpService
    {
        public ChapterHttpService(
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

        public async Task<ChapterResponse?> GetChapter(Guid id)
            => await base.GetOne<ChapterResponse>("chapter", "get", "id", id);
        public async Task<HttpResponseMessage> CreateChapter(CreateChapterRequest request)
            => await base.Create<CreateChapterRequest>(request, "chapter", "create");
        public async Task<HttpResponseMessage> UpdateChapter(UpdateChapterRequest request)
            => await base.Update(request, "chapter", "update");
        public async Task<HttpResponseMessage> DeleteChapter(Guid id)
            => await base.Delete("chapter", "delete", id);

        public async Task<HttpResponseMessage> CreateFirstSection(CreateFirstNodeRequest request)
            => await base.Create<CreateFirstNodeRequest>(request, "chapter", "section", "create", "first");
        public async Task<HttpResponseMessage> InsertSectionAfterPosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "chapter", "section", "insert", "after");
        public async Task<HttpResponseMessage> InsertSectionBeforePosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "chapter", "section", "insert", "before");
        public async Task<NodeResponse?> GetFirstSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("chapter", "section", "get", "first", id);
        public async Task<NodeResponse?> GetNextSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("chapter", "section", "get", "next", id);
        public async Task<NodeResponse?> GetLastSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("chapter", "section", "get", "last", id);
        public async Task<NodeResponse?> GetPreviousSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("chapter", "section", "get", "previous", id);
        public async Task<HttpResponseMessage> DeleteSectionAt(Guid id)
            => await base.Delete("chapter", "section", "delete", id);
    }
}

