using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Group;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class GroupService : HttpAbstractService, IGroupService
    {
        private readonly ConfigurationManager _configuration;

        public GroupService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/group/",
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

        public async Task<bool> Exists(Guid instituteId, string code)
            => await base.Exist("exists", instituteId, code);

        public async Task<HttpResponseMessage> CreateGroup(CreateGroupRequest request)
            => await base.Create(request, "create");

        public async Task<GroupModel?> GetGroupById(Guid id)
            => await base.GetOne<GroupModel>("get", "id", id);

        public async Task<GroupModel?> GetGroupByCode(Guid instituteId, string code)
            => await base.GetOne<GroupModel>("get", "code", instituteId, code);

        public async Task<int> GetGroupsCountByInstitute(Guid instituteId)
            => await base.Count("get", "count", instituteId);

        public async Task<HttpResponseMessage> GetGroupPage(GetGroupPageRequest request)
            => await base.Create<GetGroupPageRequest>(request, "get", "page");

        public async Task<HttpResponseMessage> DeleteGroup(Guid id)
            => await base.Delete("delete", id);
    }
}