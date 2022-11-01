using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Contracts.Entities.SubSection;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Document.Tester.Services
{
    internal class SubSectionHttpService : HttpService
    {
        public SubSectionHttpService(
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

        public async Task<SubSectionResponse?> GetSubSection(Guid id)
            => await base.GetOne<SubSectionResponse>("subsection", "get", "id", id);
        public async Task<HttpResponseMessage> CreateSubSection(CreateSubSectionRequest request)
            => await base.Create<CreateSubSectionRequest>(request, "subsection", "create");
        public async Task<HttpResponseMessage> UpdateSubSection(UpdateSubSectionRequest request)
            => await base.Update(request, "subsection", "update");
        public async Task<HttpResponseMessage> DeleteSubSection(Guid id)
            => await base.Delete("subsection", "delete", id);

        public async Task<HttpResponseMessage> CreateFirstSubSubSection(CreateFirstNodeRequest request)
            => await base.Create<CreateFirstNodeRequest>(request, "subsection", "subsubsection", "create", "first");
        public async Task<HttpResponseMessage> InsertSubSubSectionAfterPosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "subsection", "subsubsection", "insert", "after");
        public async Task<HttpResponseMessage> InsertSubSubSectionBeforePosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, "subsection", "subsubsection", "insert", "before");
        public async Task<NodeResponse?> GetFirstSubSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("subsection", "subsubsection", "get", "first", id);
        public async Task<NodeResponse?> GetNextSubSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("subsection", "subsubsection", "get", "next", id);
        public async Task<NodeResponse?> GetLastSubSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("subsection", "subsubsection", "get", "last", id);
        public async Task<NodeResponse?> GetPreviousSubSubSectionPosition(Guid id)
            => await base.GetOne<NodeResponse>("subsection", "subsubsection", "get", "previous", id);
        public async Task<HttpResponseMessage> DeleteSubSubSectionAt(Guid id)
            => await base.Delete("subsection", "subsubsection", "delete", id);
    }
}

