using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Holding;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services
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
                ClientId = _configuration.GetSection("Administrative.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("Administrative.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<HttpResponseMessage> CreateHolding(CreateHoldingRequest request)
            => await base.Create<CreateHoldingRequest>(request, "create");
        public async Task<HttpResponseMessage> DeleteHolding(Guid id)
            => await base.Delete("delete", id);

        public async Task<HoldingModel?> GetHoldingById(Guid id)
        {
            var item = await base.GetOne<HoldingModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }

        public async Task<HoldingModel?> GetHoldingByCode(string code)
        {
            var item = await base.GetOne<HoldingModel>("get", "code", code);
            if (item is null)
                return null;
            return item;
        }

        public async Task<int> GetHoldingCount()
            => await base.Count("get", "count");
        public async Task<HttpResponseMessage> PostPage(GetHoldingPageRequest request)
            => await base.Create<GetHoldingPageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> UpdateHolding(UpdateHoldingRequest request)
            => await base.Update(request, "update");
    }
}
