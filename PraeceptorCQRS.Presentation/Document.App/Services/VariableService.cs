using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using Microsoft.AspNetCore.Mvc;
using PraeceptorCQRS.Contracts.Entities.Variable;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class VariableService : HttpAbstractService, IVariableService
    {
        private readonly ConfigurationManager _configuration;

        public VariableService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/variable/",
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

        [HttpGet("exists/{groupId}/{code}")]
        public async Task<bool> Exists(Guid groupId, string code)
            => await base.Exist("exists", groupId, code);
        public async Task<HttpResponseMessage> CreateVariable(CreateVariableRequest request)
            => await base.Create(request, "create");
        public async Task<VariableModel?> GetVariableById(Guid id)
            => await base.GetOne<VariableModel>("get", "id", id);
        public async Task<VariableModel?> GetVariableByCode(Guid instituteId, string code)
            => await base.GetOne<VariableModel>("get", "code", instituteId, code);
        public async Task<int> GetVariableCountByGroup(Guid groupId)
            => await base.Count("get", "count", groupId);
        public async Task<HttpResponseMessage> GetVariablePage(GetVariablePageRequest request)
            => await base.Create<GetVariablePageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> DeleteVariable(Guid id)
            => await base.Delete("delete", id);

        public async Task<HttpResponseMessage> DeleteVariablesFromGroup(Guid groupId)
            => await base.Delete("delete", "from", "group", groupId);
    }
}
