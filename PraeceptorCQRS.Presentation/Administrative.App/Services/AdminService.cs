using Administrative.App.Interfaces;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Admin;
using PraeceptorCQRS.Utilities;

using System.Net;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services
{
    public class AdminService : HttpAbstractService, IAdminService
    {
        private readonly ConfigurationManager _configuration;

        public AdminService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("IdentityServer:Authority").Value}/Account/",
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

        public async Task<HttpResponseMessage> CreateAdmin(CreateAdminRequest request)
            => await base.Create<CreateAdminRequest>(request, "AdminRegister");
    }
}
