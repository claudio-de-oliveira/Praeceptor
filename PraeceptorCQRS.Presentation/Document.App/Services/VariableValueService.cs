using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.VariableValue;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class VariableValueService : HttpAbstractService, IVariableValueService
    {
        private readonly ConfigurationManager _configuration;

        public VariableValueService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/variablevalue/",
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

        public async Task<HttpResponseMessage> CreateVariableValue(CreateVariableValueRequest request)
            => await base.Create<CreateVariableValueRequest>(request, "create");
        public async Task<VariableValueModel?> GetVariableValueById(Guid id)
            => await base.GetOne<VariableValueModel>("get", "id", id);
        public async Task<VariableValueModel?> GetVariableValueByVariableAndGroupValue(Guid groupValueId, Guid variableId)
            => await base.GetOne<VariableValueModel>("get", "groupvalue", "variable", groupValueId, variableId);
        public async Task<HttpResponseMessage> UpdateVariableValue(UpdateVariableValueRequest request)
            => await base.Update(request, "update");
        public async Task<HttpResponseMessage> DeleteVariableValuesFromVariable(Guid variableId)
            => await base.Delete("delete", "from", "variable", variableId);
        public async Task<HttpResponseMessage> DeleteVariableValue(Guid id)
            => await base.Delete("delete", id);
    }
}
