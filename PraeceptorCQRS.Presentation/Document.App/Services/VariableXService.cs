using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Variable;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class VariableXService : HttpAbstractService, IVariableXService
    {
        private readonly ConfigurationManager _configuration;

        public VariableXService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/variableX/",
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

        public async Task<HttpResponseMessage> CreateVariableByHolding(CreateVariableXRequest request)
            => await base.Create<CreateVariableXRequest>(request, "create", "holding");
        public async Task<HttpResponseMessage> CreateVariableByInstitute(CreateVariableXRequest request)
            => await base.Create<CreateVariableXRequest>(request, "create", "institute");
        public async Task<HttpResponseMessage> CreateVariableByCourse(CreateVariableXRequest request)
            => await base.Create<CreateVariableXRequest>(request, "create", "course");
        public async Task<HttpResponseMessage> UpdateVariableByHolding(UpdateVariableXRequest request)
            => await base.Update<UpdateVariableXRequest>(request, "update", "holding");
        public async Task<HttpResponseMessage> UpdateVariableByInstitute(UpdateVariableXRequest request)
            => await base.Update<UpdateVariableXRequest>(request, "update", "institute");
        public async Task<HttpResponseMessage> UpdateVariableByCourse(UpdateVariableXRequest request)
            => await base.Update<UpdateVariableXRequest>(request, "update", "course");
        public async Task<HttpResponseMessage> DeleteVariable(Guid id)
        => await base.Delete("delete", id);
        public async Task<List<VariableXModel>?> GetVariablesByHolding(Guid holdingId)
        {
            var item = await base.GetMany<VariableXModel>("get", "holding", holdingId);
            if (item is null)
                return null;
            return item;
        }
        public async Task<List<VariableXModel>?> GetVariablesByInstitute(Guid instituteId)
        {
            var item = await base.GetMany<VariableXModel>("get", "institute", instituteId);
            if (item is null)
                return null;
            return item;
        }
        public async Task<List<VariableXModel>?> GetVariablesByCourseAndCurriculum(Guid courseId, int curriculum)
        {
            var item = await base.GetMany<VariableXModel>("get", "course", courseId, curriculum);
            if (item is null)
                return null;
            return item;
        }

        public async Task<HttpResponseMessage> PostPage(GetVariableXPageRequest request)
            => await base.Create<GetVariableXPageRequest>(request, "get", "page");
    }
}
