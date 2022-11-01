using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Institute;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class InstituteHttpService : HttpService
    {
        public InstituteHttpService(
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

        public async Task<InstituteResponse?> GetInstitute(Guid id)
            => await base.GetOne<InstituteResponse>("institute", "get", "id", id);
        public async Task<HttpResponseMessage> CreateInstitute(CreateInstituteRequest request)
            => await base.Create<CreateInstituteRequest>(request, "institute", "create");
        public async Task<HttpResponseMessage> UpdateInstitute(UpdateInstituteRequest request)
            => await base.Update(request, "institute", "update");
        public async Task<HttpResponseMessage> DeleteInstitute(Guid id)
            => await base.Delete("institute", "delete", id);
    }
}

