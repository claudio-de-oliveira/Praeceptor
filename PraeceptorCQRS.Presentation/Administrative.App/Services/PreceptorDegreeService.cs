using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.PreceptorDegreeType;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services
{
    public class PreceptorDegreeService : HttpAbstractService, IPreceptorDegreeService
    {
        private readonly ConfigurationManager _configuration;

        public PreceptorDegreeService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/preceptordegreetype/",
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
                ClientId = _configuration.GetSection("Administrative.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("Administrative.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<HttpResponseMessage> CreatePreceptorDegreeType(CreatePreceptorDegreeTypeRequest request)
            => await base.Create<CreatePreceptorDegreeTypeRequest>(request, "create");
        public async Task<HttpResponseMessage> DeletePreceptorDegreeType(Guid id)
            => await base.Delete("delete", id);

        public async Task<PreceptorDegreeTypeModel?> GetPreceptorDegreeTypeById(Guid id)
        {
            var item = await base.GetOne<PreceptorDegreeTypeModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }
        public async Task<PreceptorDegreeTypeModel?> GetPreceptorDegreeTypeByCode(Guid instituteId, string code)
        {
            var item = await base.GetOne<PreceptorDegreeTypeModel>("get", "code", instituteId, code);
            if (item is null)
                return null;
            return item;
        }

        public async Task<int> GetPreceptorDegreeTypeCount(Guid instituteId)
            => await base.Count("get", "count", instituteId);
        public async Task<HttpResponseMessage> PostPage(GetPreceptorDegreeTypePageRequest request)
            => await base.Create<GetPreceptorDegreeTypePageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> UpdatePreceptorDegreeType(UpdatePreceptorDegreeTypeRequest request)
            => await base.Update(request, "update");
    }
}
