using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.SubSubSection;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Document.Tester.Services
{
    internal class SubSubSectionHttpService : HttpService
    {
        public SubSubSectionHttpService(
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

        public async Task<SubSubSectionResponse?> GetSubSubSection(Guid id)
            => await base.GetOne<SubSubSectionResponse>("subsubsection", "get", "id", id);
        public async Task<HttpResponseMessage> CreateSubSubSection(CreateSubSubSectionRequest request)
            => await base.Create<CreateSubSubSectionRequest>(request, "subsubsection", "create");
        public async Task<HttpResponseMessage> UpdateSubSubSection(UpdateSubSubSectionRequest request)
            => await base.Update(request, "subsubsection", "update");
        public async Task<HttpResponseMessage> DeleteSubSubSection(Guid id)
            => await base.Delete("subsubsection", "delete", id);
    }
}

