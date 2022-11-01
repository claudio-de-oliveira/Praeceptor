using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class PreceptorRegimeTypeHttpService : HttpService
    {
        public PreceptorRegimeTypeHttpService(
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

        public async Task<PreceptorRegimeTypeResponse?> GetPreceptorRegimeType(Guid id)
            => await base.GetOne<PreceptorRegimeTypeResponse>("preceptorregimetype", "get", "id", id);
        public async Task<HttpResponseMessage> CreatePreceptorRegimeType(CreatePreceptorRegimeTypeRequest request)
            => await base.Create<CreatePreceptorRegimeTypeRequest>(request, "preceptorregimetype", "create");
        public async Task<HttpResponseMessage> UpdatePreceptorRegimeType(UpdatePreceptorRegimeTypeRequest request)
            => await base.Update(request, "preceptorregimetype", "update");
        public async Task<HttpResponseMessage> DeletePreceptorRegimeType(Guid id)
            => await base.Delete("preceptorregimetype", "delete", id);
    }
}

