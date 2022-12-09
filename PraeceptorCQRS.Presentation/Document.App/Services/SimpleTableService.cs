using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.SimpleTable;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class SimpleTableService : HttpAbstractService, ISimpleTableService
    {
        private readonly ConfigurationManager _configuration;

        public SimpleTableService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Document.API:applicationUrl").Value}/table/",
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

        public async Task<int> GetTablesCountByInstitute(Guid instituteId)
            => await base.Count("get", "count", instituteId);

        public async Task<HttpResponseMessage> GetTablePage(GetSimpleTablePageRequest request)
            => await base.Create(request, "get", "page");

        public async Task<SimpleTableModel?> GetTableById(Guid id)
            => await base.GetOne<SimpleTableModel>("get", "id", id);

        public async Task<SimpleTableModel?> GetTableByCode(string code, Guid instituteId)
            => await base.GetOne<SimpleTableModel>("get", "id", code, instituteId);

        public async Task<HttpResponseMessage> CreateTable(CreateSimpleTableRequest request)
            => await base.Create(request, "create");

        public async Task<HttpResponseMessage> UpdateTable(UpdateSimpleTableRequest request)
            => await base.Update(request, "update");

        public async Task<HttpResponseMessage> DeleteTable(Guid id)
            => await base.Delete("delete", id);
    }
}