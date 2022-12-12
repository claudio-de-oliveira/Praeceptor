using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Utilities;

using System.Net;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
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
                ClientId = _configuration.GetSection("Document.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("Document.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<InstituteModel?> GetInstituteById(Guid id)
        {
            var item = await base.GetOne<InstituteModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }
    }
}
