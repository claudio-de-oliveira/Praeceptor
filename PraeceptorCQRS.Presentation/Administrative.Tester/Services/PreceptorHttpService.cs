using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Preceptor;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class PreceptorHttpService : HttpService
    {
        public PreceptorHttpService(
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

        public async Task<PreceptorResponse?> GetPreceptor(Guid id)
            => await base.GetOne<PreceptorResponse>("preceptor", "get", "id", id);
        public async Task<HttpResponseMessage> CreatePreceptor(CreatePreceptorRequest request)
            => await base.Create<CreatePreceptorRequest>(request, "preceptor", "create");
        public async Task<HttpResponseMessage> UpdatePreceptor(UpdatePreceptorRequest request)
            => await base.Update(request, "preceptor", "update");
        public async Task<HttpResponseMessage> DeletePreceptor(Guid id)
            => await base.Delete("preceptor", "delete", id);
    }
}

