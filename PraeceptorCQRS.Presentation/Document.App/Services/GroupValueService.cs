using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.GroupValue;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class GroupValueService : HttpAbstractService, IGroupValueService
    {
        private readonly ConfigurationManager _configuration;

        public GroupValueService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/groupvalue/",
                configuration.GetSection("IdentityServer:Authority").Value,
                new HttpClient(new HttpClientHandler
                {
                    // Bypass the SSH certificate
                    ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                })
            )
        {
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

        public async Task<HttpResponseMessage> CreateGroupValue(CreateGroupValueRequest request)
            => await base.Create(request, "create");
        public async Task<GroupValueModel?> GetGroupValueById(Guid id)
            => await base.GetOne<GroupValueModel>("get", "id", id);
        public async Task<List<GroupValueModel>?> GetGroupValuesByGroup(Guid groupId)
            => await base.GetMany<GroupValueModel>("get", "group", groupId);
        public async Task<HttpResponseMessage> UpdateGroupValue(UpdateGroupValueRequest request)
            => await base.Update(request, "update");
        public async Task<HttpResponseMessage> DeleteGroupValue(Guid id)
            => await base.Delete("delete", id);

        public async Task<HttpResponseMessage> DeleteGroupValuesFromGroup(Guid groupId)
            => await base.Delete("delete", "from", "group", groupId);
    }
}
