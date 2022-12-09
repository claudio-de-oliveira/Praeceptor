using Document.App.Models;
using Document.App.Requests;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Node;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class EntityService : HttpAbstractService
    {
        private readonly ConfigurationManager _configuration;
        private readonly string _childName;

        protected EntityService(ConfigurationManager configuration, string entityName, string childName)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/{entityName}/",
                configuration.GetSection("IdentityServer:Authority").Value,
                new HttpClient(new HttpClientHandler
                {
                    // Bypass the SSH certificate
                    ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                })
            )
        {
            _childName = childName;
            _configuration = configuration;
        }

        protected override async Task<string> GetAccessToken()
        {
            var response = await _httpClient.RequestTokenAsync(new IdentityModel.Client.TokenRequest
            {
                Address = TokenEndpoint,
                GrantType = GrantTypes.ClientCredentials,
                ClientId = _configuration.GetSection("Document.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("Document.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<int> GetEntitiesCount(Guid instituteId)
            => await base.Count("get", "count", instituteId);

        public async Task<List<BookEntity>?> GetAllEntities(Guid instituteId)
        {
            var list = await base.GetMany<BookEntity>("get", "all", instituteId);
            if (list is null)
                return null;
            return list;
        }
        public async Task<List<BookEntity>?> GetEntitiesPage(Guid instituteId, int start, int count)
        {
            var list = await base.GetMany<BookEntity>("get", "page", instituteId, start, count);
            if (list is null)
                return null;
            return list;
        }
        public async Task<BookEntity?> GetEntity(Guid id)
            => await base.GetOne<BookEntity>("get", "id", id);
        public async Task<List<BookEntity>?> GetEntityList(Guid id)
            => await base.GetMany<BookEntity>("get", "list", id);

        public async Task<HttpResponseMessage> CreateEntity(CreateEntityRequest request)
            => await base.Create<CreateEntityRequest>(request, "create");
        public async Task<HttpResponseMessage> UpdateEntity(UpdateEntityRequest request)
            => await base.Update(request, "update");
        public async Task<HttpResponseMessage> DeleteEntity(Guid id)
            => await base.Delete("delete", id);

        public async Task<HttpResponseMessage> PostPage(GetEntityPageRequest request)
            => await base.Create<GetEntityPageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> CreateFirstEntity(CreateFirstNodeRequest request)
            => await base.Create<CreateFirstNodeRequest>(request, _childName, "create", "first");
        public async Task<HttpResponseMessage> InsertEntityAfterPosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, _childName, "insert", "after");
        public async Task<HttpResponseMessage> InsertEntityBeforePosition(InsertNodeRequest request)
            => await base.Create<InsertNodeRequest>(request, _childName, "insert", "before");
        public async Task<bool> MoveEntityForward(Guid parent, Guid position)
        {
            string uri = $"{_requestUri}{_childName}/move/forward/{parent}/{position}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);

            var response = await SendAsync(requestMessage);

            return await Task.FromResult(response?.StatusCode == System.Net.HttpStatusCode.NoContent);
        }
        public async Task<bool> MoveEntityBackward(Guid parent, Guid position)
        {
            string uri = $"{_requestUri}{_childName}/move/backward/{parent}/{position}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);

            var response = await SendAsync(requestMessage);

            return await Task.FromResult(response?.StatusCode == System.Net.HttpStatusCode.NoContent);
        }
        public async Task<NodeResponse?> GetFirstEntityPosition(Guid documentId, Guid id)
            => await base.GetOne<NodeResponse>(_childName, "get", "first", id);
        public async Task<NodeResponse?> GetNextEntityPosition(Guid id)
            => await base.GetOne<NodeResponse>(_childName, "get", "next", id);
        public async Task<NodeResponse?> GetLastEntityPosition(Guid documentId, Guid id)
            => await base.GetOne<NodeResponse>(_childName, "get", "last", id);
        public async Task<NodeResponse?> GetPreviousEntityPosition(Guid id)
            => await base.GetOne<NodeResponse>(_childName, "get", "previous", id);
        public async Task<HttpResponseMessage> DeleteEntityAtPosition(Guid position)
            => await base.Delete(_childName, "delete", position);

        public async Task<NodeResponse?> GetEntityPosition(Guid firstEntityId, Guid documentId, Guid secondEntityId)
        {
            var position = await GetFirstEntityPosition(documentId, firstEntityId);

            while (position is not null && position.SecondEntityId != secondEntityId)
            {
                position = await GetNextEntityPosition(position.Id);
            }

            return position;
        }
    }
}
