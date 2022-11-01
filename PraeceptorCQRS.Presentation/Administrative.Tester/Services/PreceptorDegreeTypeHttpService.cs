using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class PreceptorDegreeTypeHttpService : HttpService
    {
        public PreceptorDegreeTypeHttpService(
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

        public async Task<PreceptorDegreeTypeResponse?> GetPreceptorDegreeType(Guid id)
            => await base.GetOne<PreceptorDegreeTypeResponse>("preceptordegreetype", "get", "id", id);
        public async Task<HttpResponseMessage> CreatePreceptorDegreeType(CreatePreceptorDegreeTypeRequest request)
            => await base.Create<CreatePreceptorDegreeTypeRequest>(request, "preceptordegreetype", "create");
        public async Task<HttpResponseMessage> UpdatePreceptorDegreeType(UpdatePreceptorDegreeTypeRequest request)
            => await base.Update(request, "preceptordegreetype", "update");
        public async Task<HttpResponseMessage> DeletePreceptorDegreeType(Guid id)
            => await base.Delete("preceptordegreetype", "delete", id);
    }
}

