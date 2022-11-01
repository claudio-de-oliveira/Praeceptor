using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.AxisType;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services
{
    public class AxisTypeService : HttpAbstractService, IAxisTypeService
    {
        private readonly ConfigurationManager _configuration;

        public AxisTypeService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/axistype/",
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

        public async Task<HttpResponseMessage> CreateAxisType(CreateAxisTypeRequest request)
            => await base.Create<CreateAxisTypeRequest>(request, "create");
        public async Task<HttpResponseMessage> DeleteAxisType(Guid id)
            => await base.Delete("delete", id);

        public async Task<AxisTypeModel?> GetAxisTypeById(Guid id)
        {
            var item = await base.GetOne<AxisTypeModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }
        public async Task<AxisTypeModel?> GetAxisTypeByCode(Guid instituteId, string code)
        {
            var item = await base.GetOne<AxisTypeModel>("get", "code", instituteId, code);
            if (item is null)
                return null;
            return item;
        }

        public async Task<int> GetAxisTypeCount(Guid instituteId)
            => await base.Count("get", "count", instituteId);
        public async Task<HttpResponseMessage> GetAxisTypePage(GetAxisTypePageRequest request)
            => await base.Create<GetAxisTypePageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> UpdateAxisType(UpdateAxisTypeRequest request)
            => await base.Update(request, "update");
    }
}
