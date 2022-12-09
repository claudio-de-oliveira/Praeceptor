using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.SqlDocxStream;
using PraeceptorCQRS.Contracts.Entities.ToWord;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class DocxStreamService : HttpAbstractService, IDocxStreamService
    {
        private readonly ConfigurationManager _configuration;

        public DocxStreamService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("DocumentToWord.API:applicationUrl").Value}/api/toword/",
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

        public async Task<bool> ExistDocxStream(Guid instituteId, string code)
            => await base.Exist("exist", instituteId, code);

        public async Task<DocxModel?> GetDocxStreamById(Guid id)
        {
            var item = await base.GetOne<DocxModel>("docx", "id", id);
            if (item is null)
                return null;
            return item;
        }

        public async Task<DocxModel?> GetDocxStreamByCode(Guid instituteId, string code)
        {
            var item = await base.GetOne<DocxModel>("docx", "code", instituteId, code);
            if (item is null)
                return null;
            return item;
        }

        public async Task<int> GetDocxStreamByInstituteCount(Guid instituteId)
            => await base.Count("docx", "count", instituteId);

        public async Task<HttpResponseMessage> GetDocxStreamPage(GetDocxPageRequest request)
            => await base.Create<GetDocxPageRequest>(request, "docx", "page");

        // public async Task<HttpResponseMessage> CreateDocxStream(CreateDocxRequest request)
        //     => await base.Create<CreateDocxRequest>(request, "create");

        public async Task<HttpResponseMessage> CreateDocxStream(ConvertPpcToDocxRequest request)
            => await base.Create<ConvertPpcToDocxRequest>(request, "create");

        public async Task<HttpResponseMessage> DeleteDocxStream(Guid id)
            => await base.Delete("delete", id);
    }
}