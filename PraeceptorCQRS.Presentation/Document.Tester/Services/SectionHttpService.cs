using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.Section;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Document.Tester.Services
{
    internal class SectionHttpService : HttpService
    {
        public SectionHttpService(
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

        public async Task<SectionResponse?> GetSection(Guid id)
            => await base.GetOne<SectionResponse>("section", "get", "id", id);
        public async Task<HttpResponseMessage> CreateSection(CreateSectionRequest request)
            => await base.Create<CreateSectionRequest>(request, "section", "create");
        public async Task<HttpResponseMessage> UpdateSection(UpdateSectionRequest request)
            => await base.Update(request, "section", "update");
        public async Task<HttpResponseMessage> DeleteSection(Guid id)
            => await base.Delete("section", "delete", id);

        public async Task<HttpResponseMessage> CreateFirstSubSection(CreateFirstNodeRequest request)
            => await base.Create<CreateFirstNodeRequest>(request, "section", "subsection", "create", "first");
        public async Task<HttpResponseMessage> InsertSubSectionAfterPosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "section", "subsection", "insert", "after");
        public async Task<HttpResponseMessage> InsertSubSectionBeforePosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "section", "subsection", "insert", "before");
        public async Task<NodeResponse?> GetFirstSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("section", "subsection", "get", "first", id);
        public async Task<NodeResponse?> GetNextSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("section", "subsection", "get", "next", id);
        public async Task<NodeResponse?> GetLastSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("section", "subsection", "get", "last", id);
        public async Task<NodeResponse?> GetPreviousSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("section", "subsection", "get", "previous", id);
        public async Task<HttpResponseMessage> DeleteSubSectionAt(Guid id)
            => await base.Delete("section", "subsection", "delete", id);
    }
}

