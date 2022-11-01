using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Holding;
using PraeceptorCQRS.Utilities;

using UserManager.App.Interfaces;

using static IdentityModel.OidcConstants;

namespace UserManager.App.Services
{
    public class HoldingService : HttpAbstractService, IHoldingService
    {
        private readonly ConfigurationManager _configuration;

        public HoldingService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/holding/",
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

        public async Task<int> GetHoldingCount()
            => await base.Count("get", "count");
        public async Task<HttpResponseMessage> PostPage(GetHoldingPageRequest request)
            => await base.Create<GetHoldingPageRequest>(request, "get", "page");
    }
}
