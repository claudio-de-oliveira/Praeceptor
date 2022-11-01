using Newtonsoft.Json;

using PraeceptorCQRS.Contracts.Entities.ClassType;
using PraeceptorCQRS.Utilities;

namespace PraeceptorCQRS.Presentation.Administrative.Tester.Services
{
    internal class ClassTypeHttpService : HttpService
    {
        public ClassTypeHttpService(
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

        public async Task<ClassTypeResponse?> GetClassType(Guid id)
            => await base.GetOne<ClassTypeResponse>("classtype", "get", "id", id);
        public async Task<HttpResponseMessage> CreateClassType(CreateClassTypeRequest request)
            => await base.Create<CreateClassTypeRequest>(request, "classtype", "create");
        public async Task<HttpResponseMessage> UpdateClassType(UpdateClassTypeRequest request)
            => await base.Update(request, "classtype", "update");
        public async Task<HttpResponseMessage> DeleteClassType(Guid id)
            => await base.Delete("classtype", "delete", id);
    }
}

