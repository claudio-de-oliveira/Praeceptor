using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Institute;
using PraeceptorCQRS.Utilities;

using UserManager.App.Interfaces;

using static IdentityModel.OidcConstants;

namespace UserManager.App.Services
{
    public class InstituteService : HttpAbstractService, IInstituteService
    {
        private readonly ConfigurationManager _configuration;

        public InstituteService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/institute/",
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
                ClientId = _configuration.GetSection("UserManager.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("UserManager.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<int> GetInstituteCount(Guid holdingId)
            => await base.Count("get", "count", holdingId);
        public async Task<HttpResponseMessage> PostPage(GetInstitutePageRequest request)
            => await base.Create<GetInstitutePageRequest>(request, "get", "page");
    }
}
