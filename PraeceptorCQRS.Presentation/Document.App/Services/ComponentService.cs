using PraeceptorCQRS.Utilities;
using Document.App.Interfaces;
using IdentityModel.Client;
using static IdentityModel.OidcConstants;
using Document.App.Models;

namespace Document.App.Services
{
    public class ComponentService : HttpAbstractService, IComponentService
    {
        private readonly ConfigurationManager _configuration;

        public ComponentService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/component/",
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

        public async Task<List<CurriculumModel>?> GetCurriculumsByCourseId(Guid courseId)
            => await base.GetMany<CurriculumModel>("list", "curriculum", courseId);
    }
}
