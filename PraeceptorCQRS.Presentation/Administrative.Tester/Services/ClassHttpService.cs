using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class ClassHttpService : HttpService
    {
        public ClassHttpService(
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

        public async Task<ClassResponse?> GetClass(Guid id)
            => await base.GetOne<ClassResponse>("class", "get", "id", id);
        public async Task<HttpResponseMessage> CreateClass(CreateClassRequest request)
            => await base.Create<CreateClassRequest>(request, "class", "create");
        public async Task<HttpResponseMessage> UpdateClass(UpdateClassRequest request)
            => await base.Update(request, "class", "update");
        public async Task<HttpResponseMessage> DeleteClass(Guid id)
            => await base.Delete("class", "delete", id);
    }
}

