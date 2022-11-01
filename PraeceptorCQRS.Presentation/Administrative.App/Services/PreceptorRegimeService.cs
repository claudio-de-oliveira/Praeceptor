using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.PreceptorRegimeType;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services
{
    public class PreceptorRegimeService : HttpAbstractService, IPreceptorRegimeService
    {
        private readonly ConfigurationManager _configuration;

        public PreceptorRegimeService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/preceptorregimetype/",
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

        public async Task<HttpResponseMessage> CreatePreceptorRegimeType(CreatePreceptorRegimeTypeRequest request)
            => await base.Create<CreatePreceptorRegimeTypeRequest>(request, "create");
        public async Task<HttpResponseMessage> DeletePreceptorRegimeType(Guid id)
            => await base.Delete("delete", id);

        public async Task<PreceptorRegimeTypeModel?> GetPreceptorRegimeTypeById(Guid id)
        {
            var item = await base.GetOne<PreceptorRegimeTypeModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }
        public async Task<PreceptorRegimeTypeModel?> GetPreceptorRegimeTypeByCode(Guid instituteId, string code)
        {
            var item = await base.GetOne<PreceptorRegimeTypeModel>("get", "code", instituteId, code);
            if (item is null)
                return null;
            return item;
        }

        public async Task<int> GetPreceptorRegimeTypeCount(Guid instituteId)
            => await base.Count("get", "count", instituteId);

        public async Task<HttpResponseMessage> PostPage(GetPreceptorRegimeTypePageRequest request)
            => await base.Create<GetPreceptorRegimeTypePageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> UpdatePreceptorRegimeType(UpdatePreceptorRegimeTypeRequest request)
            => await base.Update(request, "update");
    }
}
