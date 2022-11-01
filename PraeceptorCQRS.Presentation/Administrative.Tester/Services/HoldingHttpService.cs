using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Holding;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class HoldingHttpService : HttpService
    {
        public HoldingHttpService(
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

        public async Task<HoldingResponse?> GetHolding(Guid id)
            => await base.GetOne<HoldingResponse>("holding", "get", "id", id);
        public async Task<HttpResponseMessage> CreateHolding(CreateHoldingRequest request)
            => await base.Create<CreateHoldingRequest>(request, "holding", "create");
        public async Task<HttpResponseMessage> UpdateHolding(UpdateHoldingRequest request)
            => await base.Update(request, "holding", "update");
        public async Task<HttpResponseMessage> DeleteHolding(Guid id)
            => await base.Delete("holding", "delete", id);
    }
}

